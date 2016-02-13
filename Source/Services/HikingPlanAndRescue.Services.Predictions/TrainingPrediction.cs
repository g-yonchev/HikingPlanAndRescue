using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Regression.Linear;
using HikingPlanAndRescue.Data.Common;
using HikingPlanAndRescue.Data.Models;

namespace HikingPlanAndRescue.Services.Predictions
{
    public class TrainingPrediction : ITrainingPrediction
    {
        private readonly IDbRepository<Training> trainings;

        public TrainingPrediction(IDbRepository<Training> trainings)
        {
            this.trainings = trainings;
        }

        public Training Predict(Training training)
        {
            var inputDataSet = new double[1000][];
            var outputDataSet = new double[1000][];

            var dataSet = this.trainings
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(1000)
                .ToArray();

            int i = 0;
            foreach (var dataSetTraining in dataSet)
            {
                var inputDataRow = new double[]
                {
                    dataSetTraining.Track.Length,
                    dataSetTraining.Track.Ascent,
                    dataSetTraining.Track.AscentLength,
                };

                inputDataSet[i] = inputDataRow;

                var duration = dataSetTraining.EndDate - dataSetTraining.StartDate;
                var outputDataRow = new double[]
                {
                    dataSetTraining.Calories,
                    dataSetTraining.Water,
                    duration.TotalHours
                };

                outputDataSet[i] = outputDataRow;

                i++;
            }

            var regression = new MultivariateLinearRegression(3, 3);
            double error = regression.Regress(inputDataSet, outputDataSet);

            var trainingDuration = training.EndDate - training.StartDate;
            var input = new double[]
                {
                    training.Track.Length,
                    training.Track.Ascent,
                    training.Track.AscentLength,
                };

            var result = regression.Compute(input);

            training.Calories = result[0];
            training.Water = result[1];
            training.EndDate = training.StartDate.AddHours(result[2]);

            return training;
        }
    }
}

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

        public Training PredictCaloriesAndWater(Training training)
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
                var duration = dataSetTraining.EndDate - dataSetTraining.StartDate;
                var inputDataRow = new double[]
                {
                    duration.TotalHours,
                    dataSetTraining.Track.Length,
                    dataSetTraining.Track.Ascent,
                    dataSetTraining.Track.AscentLength,
                };

                inputDataSet[i] = inputDataRow;

                var outputDataRow = new double[]
                {
                    dataSetTraining.Calories,
                    dataSetTraining.Water
                };

                outputDataSet[i] = outputDataRow;

                i++;
            }

            var regression = new MultivariateLinearRegression(4, 2);
            double error = regression.Regress(inputDataSet, outputDataSet);

            var trainingDuration = training.EndDate - training.StartDate;
            var input = new double[]
                {
                    trainingDuration.TotalHours,
                    training.Track.Length,
                    training.Track.Ascent,
                    training.Track.AscentLength,
                };

            var result = regression.Compute(input);

            training.Calories = result[0];
            training.Water = result[1];

            return training;
        }
    }
}

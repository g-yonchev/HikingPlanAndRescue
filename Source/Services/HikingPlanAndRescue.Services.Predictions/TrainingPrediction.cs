using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Regression.Linear;
using HikingPlanAndRescue.Data.Common;
using HikingPlanAndRescue.Data.Models;
using HikingPlanAndRescue.Services.Web;

namespace HikingPlanAndRescue.Services.Predictions
{
    public class TrainingPrediction : ITrainingPrediction
    {
        private ICacheService cache;
        private readonly IDbRepository<Training> trainings;

        public TrainingPrediction(IDbRepository<Training> trainings, ICacheService cache)
        {
            this.trainings = trainings;
            this.cache = cache;
        }

        public MultivariateLinearRegression GetPredictionModel(string userId)
        {
            var dataSet = this.trainings
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(1000)
                .ToArray();

            var inputDataSet = new double[dataSet.Length][];
            var outputDataSet = new double[dataSet.Length][];

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

            return regression;
        }

        public Training Predict(Training training)
        {
            var model = cache.Get(
                training.UserId,
                () =>
                {
                    return this.GetPredictionModel(training.UserId);
                }
                , 5 * 60);

            var trainingDuration = training.EndDate - training.StartDate;
            var input = new double[]
                {
                    training.Track.Length,
                    training.Track.Ascent,
                    training.Track.AscentLength,
                };

            var result = model.Compute(input);

            training.Calories = result[0];
            training.Water = result[1];
            training.EndDate = training.StartDate.AddHours(result[2]);

            return training;
        }
    }
}

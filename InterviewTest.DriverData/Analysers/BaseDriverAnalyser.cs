using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    /// <summary>
    /// Base driver analyser that 
    /// </summary>
    internal class BaseDriverAnalyser
    {
        protected readonly DriverConfiguration _driverConfiguration;

        public BaseDriverAnalyser(DriverConfiguration driverConfiguration)
        {
            _driverConfiguration = driverConfiguration;
        }

        /// <summary>
        /// Calculates final rating and total duration for analysed periods
        /// </summary>
        /// <param name="periodAnalysisList">List of analysed periods</param>
        /// <returns>Computed driver rating and total duration, or zero rating and zero duration for empty or null input</returns>
        protected virtual HistoryAnalysis ComputeHistoryAnalysis(IEnumerable<PeriodAnalysis> periodAnalysisList)
        {
            var historyAnalysisResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (periodAnalysisList != null && periodAnalysisList.Any())
            {
                periodAnalysisList = periodAnalysisList.OrderBy(periodAnalysis => periodAnalysis.StartTime);

                var weightedSum = periodAnalysisList.Select(periodAnalysis => periodAnalysis.Duration * periodAnalysis.Rating).Sum();
                historyAnalysisResult.DriverRating = weightedSum > 0 ? weightedSum / periodAnalysisList.Sum(periodAnalysis => periodAnalysis.Duration) : 0;

                if (_driverConfiguration.IsPenaltyApplicable)
                {
                    historyAnalysisResult.DriverRating = historyAnalysisResult.DriverRating * _driverConfiguration.PenaltyForUndocumentedPeriod;
                }

                var documentedDuration = periodAnalysisList.Where(periodAnalysis => !periodAnalysis.IsUndocumented).Sum(periodAnalysis => periodAnalysis.Duration);
                historyAnalysisResult.AnalysedDuration = new TimeSpan(0, 0, (int)documentedDuration);
            }

            return historyAnalysisResult;
        }

        /// <summary>
        /// Computes driver rating and duration for each undocumented period
        /// </summary>
        /// <param name="validPeriodList">A list of valid periods to calculate driver rating and duration</param>
        /// <returns>An System.Collections.Generic.IEnumerable that contains analysis result of input list of periods</returns>
        protected virtual IEnumerable<PeriodAnalysis> AnalyseUndocumentedPeriodList(IEnumerable<Period> validPeriodList)
        {
            var undocumentedPeriodAnalysisList = new List<PeriodAnalysis>();

            var validPeriodCount = validPeriodList.Count();
            for (int i = 0; i < validPeriodCount; i++)
            {
                // If first period start time is greater than allowed start time then get this time difference as undocumented period
                if (i == 0 && validPeriodList.ElementAt(i).Start.TimeOfDay > _driverConfiguration.StartTime)
                {
                    decimal duration = (decimal)(validPeriodList.ElementAt(i).Start.TimeOfDay - _driverConfiguration.StartTime).TotalSeconds;
                    undocumentedPeriodAnalysisList.Add(new PeriodAnalysis { StartTime = _driverConfiguration.StartTime, EndTime = validPeriodList.ElementAt(i).Start.TimeOfDay, Duration = duration, Rating = 0, IsUndocumented = true });
                }
                // If last period end time is less than allowed end time then get the time difference as undocumented period
                else if (i == validPeriodCount - 1 && validPeriodList.ElementAt(i).End.TimeOfDay < _driverConfiguration.EndTime)
                {
                    decimal duration = (decimal)(_driverConfiguration.EndTime - validPeriodList.ElementAt(i).End.TimeOfDay).TotalSeconds;
                    undocumentedPeriodAnalysisList.Add(new PeriodAnalysis() { StartTime = validPeriodList.ElementAt(i).End.TimeOfDay, EndTime = _driverConfiguration.EndTime, Duration = duration, Rating = 0, IsUndocumented = true });
                }
                // If current driver period start time is greater than end time of previous period, then get time difference as undocumented period
                else if (i > 0 && validPeriodList.ElementAt(i).Start.TimeOfDay > _driverConfiguration.StartTime && validPeriodList.ElementAt(i).Start.TimeOfDay > validPeriodList.ElementAt(i - 1).End.TimeOfDay)
                {
                    decimal duration = (decimal)(validPeriodList.ElementAt(i).Start.TimeOfDay - validPeriodList.ElementAt(i - 1).End.TimeOfDay).TotalSeconds;
                    undocumentedPeriodAnalysisList.Add(new PeriodAnalysis { StartTime = validPeriodList.ElementAt(i - 1).End.TimeOfDay, EndTime = validPeriodList.ElementAt(i).Start.TimeOfDay, Duration = duration, Rating = 0, IsUndocumented = true });
                }
            }

            return undocumentedPeriodAnalysisList;
        }

        /// <summary>
        /// Computes driver rating and duration for each valid period
        /// </summary>
        /// <param name="validPeriodList">A list of valid periods</param>
        /// <returns>An System.Collections.Generic.IEnumerable that contains analysis result of input list of periods</returns>
        protected virtual IEnumerable<PeriodAnalysis> AnalyseValidPeriodList(IEnumerable<Period> validPeriodList)
        {
            var periodAnalysisList = new List<PeriodAnalysis>();

            if (validPeriodList != null && validPeriodList.Any())
            {
                foreach (var period in validPeriodList)
                {
                    var periodAnalysis = new PeriodAnalysis();
                    periodAnalysis.StartTime = period.Start.TimeOfDay < _driverConfiguration.StartTime ? _driverConfiguration.StartTime : period.Start.TimeOfDay;
                    periodAnalysis.EndTime = period.End.TimeOfDay > _driverConfiguration.EndTime ? _driverConfiguration.EndTime : period.End.TimeOfDay;
                    periodAnalysis.Duration = (decimal)(periodAnalysis.EndTime - periodAnalysis.StartTime).TotalSeconds;

                    // When period average speed is greater than maximum speed then set rating to 0, else calculate rating based on average and maximum speed
                    periodAnalysis.Rating = period.AverageSpeed > _driverConfiguration.MaxSpeed ?
                                            _driverConfiguration.RatingForExceedingSpeedLimit : period.AverageSpeed / _driverConfiguration.MaxSpeed;
                    
                    periodAnalysisList.Add(periodAnalysis);
                }
            }

            return periodAnalysisList;
        }

        /// <summary>
        /// Gets valid periods
        /// </summary>
        /// <param name="history">A readonly collection of driving periods</param>
        /// <returns>An System.Collections.Generic.IEnumerable`1 that contains valid periods</returns>
        protected virtual IEnumerable<Period> GetValidPeriods(IEnumerable<Period> history)
        {
            return history.Where(period => period.End.TimeOfDay > _driverConfiguration.StartTime && period.Start.TimeOfDay < _driverConfiguration.EndTime).OrderBy(x => x.Start);
        }
    }
}

using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    internal class DeliveryDriverAnalyser : IAnalyser
    {
        private readonly DriverConfiguration _driverConfiguration;

        public DeliveryDriverAnalyser(DriverConfiguration driverConfiguration)
        {
            _driverConfiguration = driverConfiguration;
        }

        /// <summary>
        /// Analyses the driver period history and computes driver final rating and total duration
        /// </summary>
        /// <param name="history">A readonly collection of driving period</param>
        /// <returns></returns>
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            //Initialize default analysis result
            var historyAnalysisResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (history != null && history.Any())
            {
                var validPeriods = GetValidPeriods(history);

                var periodAnalysisList = AnalyseValidPeriodList(validPeriods);
                var undocumentedPeriods = AnalyseUndocumentedPeriodList(validPeriods);
                periodAnalysisList = periodAnalysisList.Concat(undocumentedPeriods);

                historyAnalysisResult = ComputeHistoryAnalysis(periodAnalysisList);
            }

            return historyAnalysisResult;
        }

        /// <summary>
        /// Calculates final rating and total duration for analysed periods
        /// </summary>
        /// <param name="periodAnalysisList">List of analysed periods</param>
        /// <returns>Computed driver rating and total duration, or zero rating and zero duration for empty or null input</returns>
        private HistoryAnalysis ComputeHistoryAnalysis(IEnumerable<PeriodAnalysis> periodAnalysisList)
        {
            var historyAnalysisResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (periodAnalysisList != null && periodAnalysisList.Any())
            {
                periodAnalysisList = periodAnalysisList.OrderBy(item => item.StartTime);

                var weightedSum = periodAnalysisList.Select(periodAnalysis => periodAnalysis.Duration * periodAnalysis.Rating).Sum();
                historyAnalysisResult.DriverRating = weightedSum > 0 ? weightedSum / periodAnalysisList.Sum(periodAnalysis => periodAnalysis.Duration) : 0;

                var documentedDuration = periodAnalysisList.Where(periodAnalysis => !periodAnalysis.IsUndocumented).Sum(periodAnalysis => periodAnalysis.Duration);
                historyAnalysisResult.AnalysedDuration = new TimeSpan(0, 0, (int)documentedDuration);
            }

            return historyAnalysisResult;
        }

        /// <summary>
        /// Computes driver rating and duration for each undocumented period
        /// </summary>
        /// <param name="validPeriodList">A list of valid periods to calculate driver rating and duration</param>
        /// <returns>An System.Collections.Generic.IEnumerable`1 that contains analysis result of input list of periods</returns>
        private IEnumerable<PeriodAnalysis> AnalyseUndocumentedPeriodList(IEnumerable<Period> validPeriodList)
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
                // If current driver period start time is less than end time of previous period, then get time difference as undocumented period
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
        /// <returns>An System.Collections.Generic.IEnumerable`1 that contains analysis result of input list of periods</returns>
        private IEnumerable<PeriodAnalysis> AnalyseValidPeriodList(IEnumerable<Period> validPeriodList)
        {
            var periodAnalysisList = new List<PeriodAnalysis>();

            if (validPeriodList != null && validPeriodList.Any())
            {
                foreach (var period in validPeriodList)
                {
                    var periodAnalysis = new PeriodAnalysis();
                    periodAnalysis.StartTime = period.Start.TimeOfDay;
                    periodAnalysis.EndTime = period.End.TimeOfDay;
                    periodAnalysis.Duration = (decimal)(period.End - period.Start).TotalSeconds;

                    // When period average speed is greater than maximum speed then set rating to 0, else calculate rating based on average and maximum speed
                    periodAnalysis.Rating = period.AverageSpeed > _driverConfiguration.MaxSpeed ?
                                            0 : period.AverageSpeed / _driverConfiguration.MaxSpeed;

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
        private IEnumerable<Period> GetValidPeriods(IReadOnlyCollection<Period> history)
        {
            var validPeriods = history.Where(period => period.End.TimeOfDay > _driverConfiguration.StartTime && period.Start.TimeOfDay < _driverConfiguration.EndTime).OrderBy(x => x.Start);

            if (validPeriods.Any())
            {
                //If the first period starts before the start time of analyser then set the period start time to analyser start time.
                if (validPeriods.First().Start.TimeOfDay < _driverConfiguration.StartTime)
                {
                    validPeriods.First().Start = validPeriods.First().Start.Add(_driverConfiguration.StartTime - validPeriods.First().Start.TimeOfDay);
                }

                //If the last period ends after the end time of analyser then set the period end time to analyser end time.
                if (validPeriods.Last().End.TimeOfDay > _driverConfiguration.EndTime)
                {
                    validPeriods.Last().End = validPeriods.Last().End.Subtract(validPeriods.Last().End.TimeOfDay - _driverConfiguration.EndTime);
                }
            }

            return validPeriods;
        }
    }
}
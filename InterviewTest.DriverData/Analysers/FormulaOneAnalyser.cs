using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // Internal class is accessible within same assembly only.
    // As a part of dependency inversion principle, we should program to interface rather than implementation.
    // So clients of this feature can use IAnalyser interface and need not worry about which concrete implementation is available to them.
    internal class FormulaOneAnalyser : BaseDriverAnalyser, IAnalyser
	{
        public FormulaOneAnalyser(DriverConfiguration driverConfiguration) : base(driverConfiguration) { }
       
        /// <summary>
        /// Analyses the driver period history and computes driver final rating and total duration
        /// </summary>
        /// <param name="history">A readonly collection of driving periods</param>
        /// <returns></returns>
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
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
        /// Computes driver rating and duration for each undocumented period
        /// </summary>
        /// <param name="validPeriodList">A list of valid periods to calculate driver rating and duration</param>
        /// <returns>An IEnumerable that contains analysis result for input list of periods</returns>
        protected override IEnumerable<PeriodAnalysis> AnalyseUndocumentedPeriodList(IEnumerable<Period> validPeriodList)
        {   
            var undocumentedPeriodAnalysisList = new List<PeriodAnalysis>();

            var validPeriodCount = validPeriodList.Count();
            for (int i = 1; i < validPeriodCount; i++)
            {
                // If current driver period start time is greater than end time of previous period, then get time difference as undocumented period
                if (validPeriodList.ElementAt(i).Start.TimeOfDay > _driverConfiguration.StartTime && validPeriodList.ElementAt(i).Start.TimeOfDay > validPeriodList.ElementAt(i - 1).End.TimeOfDay)
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
        /// <returns>An IEnumerable that contains analysis result for input list of periods</returns>
        protected override IEnumerable<PeriodAnalysis> AnalyseValidPeriodList(IEnumerable<Period> validPeriodList)
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
        /// <returns>An IEnumerable that contains valid periods</returns>
        protected override IEnumerable<Period> GetValidPeriods(IEnumerable<Period> history)
        {
            var filteredPeriods = history.SkipWhile(period => period.AverageSpeed <= 0).ToList();

            if (!filteredPeriods.Any())
                return filteredPeriods;

            var lastNonZeroPeriodIndex = filteredPeriods.IndexOf(filteredPeriods.LastOrDefault(p => p.AverageSpeed > 0));

            return filteredPeriods.Take(lastNonZeroPeriodIndex + 1);
        }

    }
}
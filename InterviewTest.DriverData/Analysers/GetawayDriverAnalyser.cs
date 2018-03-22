using System;
using System.Collections.Generic;
using InterviewTest.DriverData.Entities;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // Internal class is accessible within same assembly only.
    // As a part of dependency inversion principle, we should program to interface rather than implementation.
    // So clients of this feature can use IAnalyser interface and need not worry about which concrete implementation is available to them.
    internal class GetawayDriverAnalyser : BaseDriverAnalyser, IAnalyser
    {
        public GetawayDriverAnalyser(DriverConfiguration driverConfiguration) : base(driverConfiguration) { }

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

            return base.GetValidPeriods(filteredPeriods.Take(lastNonZeroPeriodIndex + 1));
        }
    }
}
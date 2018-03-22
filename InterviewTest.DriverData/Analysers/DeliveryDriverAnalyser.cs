using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // Internal class is accessible within same assembly only.
    // As a part of dependency inversion principle, we should program to interface rather than implementation.
    // So clients of this feature can use IAnalyser interface and need not worry about what is actual concrete implementation.
    internal class DeliveryDriverAnalyser : BaseDriverAnalyser, IAnalyser
    {
        public DeliveryDriverAnalyser(DriverConfiguration driverConfiguration) : base(driverConfiguration) { }

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
    }
}
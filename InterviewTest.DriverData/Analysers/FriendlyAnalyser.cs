using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    // Internal class is accessible within same assembly only.
    // As a part of dependency inversion principle, we should program to interface rather than implementation.
    // So clients of this feature can use IAnalyser interface and need not worry about which concrete implementation is available to them.
    internal class FriendlyAnalyser : IAnalyser
	{
		public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
			return new HistoryAnalysis
			{
				AnalysedDuration = history.Last().End - history.First().Start,
				DriverRating = 1m
			};
		}
	}
}
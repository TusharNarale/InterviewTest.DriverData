using System;
using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
		public static IAnalyser GetAnalyser(string type)
		{
			switch (type)
			{
				case "friendly":
					return new FriendlyAnalyser();
                case "delivery":
                    return new DeliveryDriverAnalyser(new DriverConfiguration() { StartTime = new TimeSpan(9,0,0), EndTime = new TimeSpan(17,0,0), MaxSpeed = 30m, RatingForExceedingSpeedLimit = 0.0m, IsPenaltyApplicable = true, PenaltyForUndocumentedPeriod = 0.5m });
                case "formulaone":
                    return new FormulaOneAnalyser(new DriverConfiguration() { StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, MaxSpeed = 200m, RatingForExceedingSpeedLimit = 1.0m, IsPenaltyApplicable = true, PenaltyForUndocumentedPeriod = 0.5m });
                case "getaway":
                    return new GetawayDriverAnalyser(new DriverConfiguration() { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0), MaxSpeed = 80m, RatingForExceedingSpeedLimit = 1.0m, IsPenaltyApplicable = true, PenaltyForUndocumentedPeriod = 0.5m });
                default:
					throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
			}
		}
	}
}

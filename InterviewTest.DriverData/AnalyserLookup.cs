using InterviewTest.DriverData.Analysers;
using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
    public static class AnalyserLookup
    {
        public static IAnalyser GetAnalyser(string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type), "Null parser type");

            if (!analyserList.ContainsKey(type))
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");

                return analyserList[type]();
        }
        
        private static Dictionary<string, Func<IAnalyser>> analyserList =
                    new Dictionary<string, Func<IAnalyser>>
                {
                    { "delivery", DeliveryDriverAnalyser },
                    { "friendly", FriendlyAnalyser },
                    { "formulaone", FormulaOneAnalyser },
                    { "getaway", GetawayDriverAnalyser }
                };

        private static DeliveryDriverAnalyser DeliveryDriverAnalyser()
        {
            var driverConfiguration = new DriverConfiguration() {
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                MaxSpeed = 30m,
                RatingForExceedingSpeedLimit = 0.0m,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            };
            return new DeliveryDriverAnalyser(driverConfiguration);
        }

        private static FriendlyAnalyser FriendlyAnalyser()
        {
            return new FriendlyAnalyser();
        }

        private static FormulaOneAnalyser FormulaOneAnalyser()
        {
            var driverConfiguration = new DriverConfiguration()
            {
                StartTime = TimeSpan.Zero,
                EndTime = TimeSpan.Zero,
                MaxSpeed = 200m,
                RatingForExceedingSpeedLimit = 1.0m,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            };

            return new FormulaOneAnalyser(driverConfiguration);
        }

        private static GetawayDriverAnalyser GetawayDriverAnalyser()
        {
            var driverConfiguration = new DriverConfiguration()
            {
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(14, 0, 0),
                MaxSpeed = 80m,
                RatingForExceedingSpeedLimit = 1.0m,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            };

            return new GetawayDriverAnalyser(driverConfiguration);
        }
    }
}

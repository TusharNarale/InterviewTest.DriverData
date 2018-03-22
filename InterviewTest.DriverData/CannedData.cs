using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using InterviewTest.DriverData.Lookups;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace InterviewTest.DriverData
{
    public static class CannedDrivingData
    {
        private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

        private static string historyFilePath = ConfigurationManager.AppSettings.Get("HistoryFilePath");

        public static IReadOnlyCollection<Period> GetHistoryFromFile()
        {
            string fileData = GetHistoryFileData();

            return ParseHistoryFileData(fileData);
        }

        private static IReadOnlyCollection<Period> ParseHistoryFileData(string fileData)
        {
            var dataParser = DataParserLookup.GetParser("json");
            return dataParser.ParseData<IReadOnlyCollection<Period>>(fileData);
        }

        private static string GetHistoryFileData()
        {
            var dataReader = DataReaderLookup.GetReader("file");
            var fileData = dataReader.ReadData(historyFilePath);
            return fileData;
        }

        // BONUS: What's so great about IReadOnlyCollections?
        public static readonly IReadOnlyCollection<Period> History = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(8, 54, 0),
                End = _day + new TimeSpan(9, 28, 0),
                AverageSpeed = 28m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 28, 0),
                End = _day + new TimeSpan(9, 35, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(9, 50, 0),
                End = _day + new TimeSpan(12, 35, 0),
                AverageSpeed = 25m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 35, 0),
                End = _day + new TimeSpan(13, 30, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(13, 30, 0),
                End = _day + new TimeSpan(19, 12, 0),
                AverageSpeed = 29m
            },
            new Period
            {
                Start = _day + new TimeSpan(19, 12, 0),
                End = _day + new TimeSpan(24, 0, 0),
                AverageSpeed = 0m
            }
        };

        public static readonly IReadOnlyCollection<Period> EmptyHistory = new Period[] { };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverNonPermittedTimePeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(2, 0, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(3, 10, 0),
                End = _day + new TimeSpan(4, 0, 0),
                AverageSpeed = 28m
            },
            new Period
            {
                Start = _day + new TimeSpan(5, 20, 0),
                End = _day + new TimeSpan(7, 0, 0),
                AverageSpeed = 33m
            },
            new Period
            {
                Start = _day + new TimeSpan(7, 12, 0),
                End = _day + new TimeSpan(8, 59, 0),
                AverageSpeed = 12m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverExceedingSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(11, 0, 0),
                AverageSpeed = 32m
            },
            new Period
            {
                Start = _day + new TimeSpan(11, 10, 0),
                End = _day + new TimeSpan(13, 30, 0),
                AverageSpeed = 40m
            },
            new Period
            {
                Start = _day + new TimeSpan(14, 40, 0),
                End = _day + new TimeSpan(15, 0, 0),
                AverageSpeed = 35m
            },
            new Period
            {
                Start = _day + new TimeSpan(15, 09, 0),
                End = _day + new TimeSpan(16, 59, 0),
                AverageSpeed = 45m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverMaxSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 30m
            }
        };


        public static readonly IReadOnlyCollection<Period> DriverSameStartAndEndTimePeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(9, 0, 0),
                AverageSpeed = 30m
            }
        };


        public static readonly IReadOnlyCollection<Period> DeliveryDriverStartTimeGreaterThanAllowedTimeWithEndTimeGreaterThanAllowedTimeAndMaxSpeedLimit = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(8, 0, 0),
                End = _day + new TimeSpan(18, 0, 0),
                AverageSpeed = 30m
            }
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverMaxSpeedLimitWithUndocumentedPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(11, 0, 0),
                AverageSpeed = 30m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 30m
            }
        };

        public static readonly IReadOnlyCollection<Period> FormulaOneDriverWithZeroAverageSpeedPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(2, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(3, 10, 0),
                End = _day + new TimeSpan(4, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(5, 20, 0),
                End = _day + new TimeSpan(7, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(7, 12, 0),
                End = _day + new TimeSpan(8, 59, 0),
                AverageSpeed = 0m
            }
        };

        public static readonly IReadOnlyCollection<Period> FormulaOneDriverMaxSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 200m
            }
        };

        public static readonly IReadOnlyCollection<Period> FormulaOneDriverExceedingMaxSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 210m
            }
        };


        public static readonly IReadOnlyCollection<Period> FormulaOneDriverMaxSpeedLimitWithUndocumentedPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(11, 0, 0),
                AverageSpeed = 200m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 200m
            }
        };

        public static readonly IReadOnlyCollection<Period> GetawayDriverWithZeroAverageSpeedPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(2, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(3, 10, 0),
                End = _day + new TimeSpan(4, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(5, 20, 0),
                End = _day + new TimeSpan(7, 0, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(7, 12, 0),
                End = _day + new TimeSpan(8, 59, 0),
                AverageSpeed = 0m
            }
        };

        public static readonly IReadOnlyCollection<Period> GetawayDriverMaxSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 80m
            }
        };

        public static readonly IReadOnlyCollection<Period> GetawayDriverExceedingMaxSpeedLimitPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 85m
            }
        };

        public static readonly IReadOnlyCollection<Period> GetawayDriverMaxSpeedLimitWithUndocumentedPeriodHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(11, 0, 0),
                AverageSpeed = 80m
            },
            new Period
            {
                Start = _day + new TimeSpan(12, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 80m
            }
        };
    }

}

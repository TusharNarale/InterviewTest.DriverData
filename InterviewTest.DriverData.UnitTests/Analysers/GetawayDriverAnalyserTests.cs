using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
	{
        private GetawayDriverAnalyser _getawayDriverAnalyser;
        private GetawayDriverAnalyser _getawayDriverAnalyserWithPenaltyApplicable;

        [SetUp]
        public void Initialize()
        {
            _getawayDriverAnalyser = new GetawayDriverAnalyser(new DriverConfiguration
            {
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(14, 0, 0),
                MaxSpeed = 80m,
                RatingForExceedingSpeedLimit = 1
            });

            _getawayDriverAnalyserWithPenaltyApplicable = new GetawayDriverAnalyser(new DriverConfiguration
            {
                StartTime = new TimeSpan(13, 0, 0),
                EndTime = new TimeSpan(14, 0, 0),
                MaxSpeed = 80m,
                RatingForExceedingSpeedLimit = 1,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            });
        }

        [TearDown]
        public void TearDown()
        {
            _getawayDriverAnalyser = null;
            _getawayDriverAnalyserWithPenaltyApplicable = null;
        }


        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromHours(1),
				DriverRating = 0.1813m
			};

			var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}


        [Test]
        public void ShouldYieldZeroRating_ForNullHistory()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(null);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForEmptyHistory()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.EmptyHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithZeroSpeed()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.GetawayDriverWithZeroAverageSpeedPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithSameStartAndEndTime()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.DriverSameStartAndEndTimePeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForSinglePeriodWithMaxSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.GetawayDriverMaxSpeedLimitPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForSinglePeriodWithSpeedExceedingMaxSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.GetawayDriverMaxSpeedLimitPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }


        [Test]
        public void ShouldYieldCorrectValues_WhenPenaltyIsApplicable()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.1813m * 0.5m
            };

            var actualResult = _getawayDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.History);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForMaxSpeedLimit_WithUndocumentedPeriods_WhenPenaltyIsApplicable()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 0.5m,
            };

            var actualResult = _getawayDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.GetawayDriverMaxSpeedLimitWithUndocumentedPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForHistoryLoadedFromFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.1813m
            };

            var actualResult = _getawayDriverAnalyser.Analyse(CannedDrivingData.GetHistoryFromFile());

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}

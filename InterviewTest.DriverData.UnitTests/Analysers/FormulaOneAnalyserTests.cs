using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
        private FormulaOneAnalyser _formulaOneDriverAnalyser;
        private FormulaOneAnalyser _formulaOneDriverAnalyserWithPenaltyApplicable;
        
        [SetUp]
        public void Initialize()
        {
            _formulaOneDriverAnalyser = new FormulaOneAnalyser(new DriverConfiguration
            {
                StartTime = TimeSpan.Zero,
                EndTime = TimeSpan.Zero,
                MaxSpeed = 200m,
                RatingForExceedingSpeedLimit = 1
            });

            _formulaOneDriverAnalyserWithPenaltyApplicable = new FormulaOneAnalyser(new DriverConfiguration
            {
                StartTime = TimeSpan.Zero,
                EndTime = TimeSpan.Zero,
                MaxSpeed = 200m,
                RatingForExceedingSpeedLimit = 1,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            });
        }

        [TearDown]
        public void TearDown()
        {
            _formulaOneDriverAnalyser = null;
            _formulaOneDriverAnalyserWithPenaltyApplicable = null;
        }

        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m
			};

			var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.History);

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

            var actualResult = _formulaOneDriverAnalyser.Analyse(null);

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

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.EmptyHistory);

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

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.FormulaOneDriverWithZeroAverageSpeedPeriodHistory);

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

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.DriverSameStartAndEndTimePeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForSinglePeriodWithMaxSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.FormulaOneDriverMaxSpeedLimitPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForSinglePeriodWithSpeedExceedingMaxSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.FormulaOneDriverMaxSpeedLimitPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }


        [Test]
        public void ShouldYieldCorrectValues_WhenPenaltyIsApplicable()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 03, 0),
                DriverRating = 0.1231m * 0.5m
            };

            var actualResult = _formulaOneDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.History);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForMaxSpeedLimit_WithUndocumentedPeriods_WhenPenaltyIsApplicable()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 0, 0),
                DriverRating = 0.4375m,
            };

            var actualResult = _formulaOneDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.FormulaOneDriverMaxSpeedLimitWithUndocumentedPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForHistoryLoadedFromFile()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };

            var actualResult = _formulaOneDriverAnalyser.Analyse(CannedDrivingData.GetHistoryFromFile());

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}

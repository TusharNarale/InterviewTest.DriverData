using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
    [TestFixture]
    public class DeliveryDriverAnalyserTests
    {
        private DeliveryDriverAnalyser _deliveryDriverAnalyser;
        private DeliveryDriverAnalyser _deliveryDriverAnalyserWithPenaltyApplicable;

        [SetUp]
        public void Initialize()
        {
            _deliveryDriverAnalyser = new DeliveryDriverAnalyser(new DriverConfiguration
            {
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                MaxSpeed = 30m
            });

            _deliveryDriverAnalyserWithPenaltyApplicable = new DeliveryDriverAnalyser(new DriverConfiguration
            {
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                MaxSpeed = 30m,
                IsPenaltyApplicable = true,
                PenaltyForUndocumentedPeriod = 0.5m
            });
        }

        [TearDown]
        public void TearDown()
        {
            _deliveryDriverAnalyser = null;
            _deliveryDriverAnalyserWithPenaltyApplicable = null;
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.History);
            
            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }


        [Test]
        public void ShouldYieldZeroRating_ForNullHistory()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(null);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForEmptyHistory()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.EmptyHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithNonPermittedTime()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverNonPermittedTimePeriodHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithExceedingSpeedLimit()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 30, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverExceedingSpeedLimitPeriodHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithSameStartAndEndTime()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.DriverSameStartAndEndTimePeriodHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForSinglePeriodWithMaxSpeedLimit_WithAllowedStartAndEndTimes()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverMaxSpeedLimitPeriodHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForSinglePeriodWithStartTimeGreaterThanAllowedStartTime_WithEndTimeGreaterThanAllowedEndTime_WithMaxSpeedLimit()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverStartTimeGreaterThanAllowedTimeWithEndTimeGreaterThanAllowedTimeAndMaxSpeedLimit);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldCorrectValues_WhenPenaltyIsApplicable()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m * 0.5m
            };

            // Act
            var actualResult = _deliveryDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.History);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForMaxSpeedLimit_WithUndocumentedPeriods_WhenPenaltyIsApplicable()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 0, 0),
                DriverRating = 0.4375m
            };

            // Act
            var actualResult = _deliveryDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.DeliveryDriverMaxSpeedLimitWithUndocumentedPeriodHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForHistoryLoadedFromFile()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            // Act
            var actualResult = _deliveryDriverAnalyser.Analyse(CannedDrivingData.GetHistoryFromFile());
            
            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

    }
}
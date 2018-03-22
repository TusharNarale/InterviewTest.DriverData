using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using InterviewTest.DriverData.Entities;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
    [TestFixture]
    public class DeliveryDriverAnalyserTests
    {
        private DeliveryDriverAnalyser deliveryDriverAnalyser;
        private readonly DriverConfiguration driverConfiguration = new DriverConfiguration
        {   
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(17, 0, 0),
            MaxSpeed = 30m
        };

        [SetUp]
        public void Initialize()
        {
            deliveryDriverAnalyser = new DeliveryDriverAnalyser(driverConfiguration);
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.History);

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

            var actualResult = deliveryDriverAnalyser.Analyse(null);

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

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.EmptyHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithNonPermittedTime()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverNonPermittedTimePeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithExceedingSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(6, 30, 0),
                DriverRating = 0.0m
            };

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverExceedingSpeedLimitPeriodHistory);

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

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.DriverSameStartAndEndTimePeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldOneRating_ForPeriodsWithMaxSpeedLimit_WithAllowedStartAndEndTimes()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverMaxSpeedLimitPeriodHistory);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldCorrectValues_ForPeriodsWithStartTimeGreaterThanAllowedStartTime_WithEndTimeGreaterThanAllowedEndTime_WithMaxSpeedLimit()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(8, 0, 0),
                DriverRating = 1.0m
            };

            var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.DeliveryDriverStartTimeGreaterThanAllowedTimeWithEndTimeGreaterThanAllowedTimeAndMaxSpeedLimit);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }
    }
}
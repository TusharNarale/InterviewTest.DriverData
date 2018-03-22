using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FriendlyAnalyserTests
	{
		[Test]
		public void ShouldAnalyseWholePeriodAndReturn1ForDriverRating()
		{
            // BONUS: What is AAA?
            /* It represents unit test writing mechanism which comprises of Arrange, Act and then Assert.
                Arrange part comprises of preparing/setting up data before testing a method/unit.
                Act part comprises of calling a unit/method to test
                Assert part comprises of validating/asserting the expected output/behaviour.
             */

            // Arrange
            var data = new[]
			{
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					AverageSpeed = 20m
				},
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 2, 0, 0, 0, TimeSpan.Zero),
					AverageSpeed = 15m
				}
			};

			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromDays(1),
				DriverRating = 1m
			};

            // Act
			var actualResult = new FriendlyAnalyser().Analyse(data);
            
            // Assert
			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
		}
	}
}

using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using System;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class AnalyserLookupTests
    {
        [Test]
        public void ShouldReturnFriendlyAnalyserForValidInput()
        {
            // Arrange
            var analyserType = "friendly";
            
            // Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);

            // Assert
            Assert.IsInstanceOf(typeof(FriendlyAnalyser), analyser);
        }


        [Test]
        public void ShouldReturnDeliveryDriverAnalyserForValidInput()
        {
            // Arrange
            var analyserType = "delivery";

            // Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);

            // Assert
            Assert.IsInstanceOf(typeof(DeliveryDriverAnalyser), analyser);
        }

        [Test]
        public void ShouldReturnGetawayDriverAnalyserForValidInput()
        {
            // Arrange
            var analyserType = "getaway";

            // Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);

            // Assert
            Assert.IsInstanceOf(typeof(GetawayDriverAnalyser), analyser);
        }

        [Test]
        public void ShouldReturnFormulaOneDriverAnalyserForValidInput()
        {
            // Arrange
            var analyserType = "formulaone";

            // Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);

            // Assert
            Assert.IsInstanceOf(typeof(FormulaOneAnalyser), analyser);
        }
        
        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            // Arrange
            var analyserType = "";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AnalyserLookup.GetAnalyser(analyserType));
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => AnalyserLookup.GetAnalyser(null));
        }

        [Test]
        public void ShouldThrowExceptionForInvalidInput()
        {
            var analyserType = "invalidDriver";

            Assert.Throws<ArgumentOutOfRangeException>(() => AnalyserLookup.GetAnalyser(analyserType));
        }
    }
}

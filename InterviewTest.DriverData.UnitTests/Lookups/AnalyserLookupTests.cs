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
            var analyserType = "friendly";
            
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            Assert.IsInstanceOf(typeof(FriendlyAnalyser), analyser);
        }


        [Test]
        public void ShouldReturnDeliveryDriverAnalyserForValidInput()
        {
            var analyserType = "delivery";

            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            Assert.IsInstanceOf(typeof(DeliveryDriverAnalyser), analyser);
        }

        [Test]
        public void ShouldReturnGetawayDriverAnalyserForValidInput()
        {
            var analyserType = "getaway";

            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            Assert.IsInstanceOf(typeof(GetawayDriverAnalyser), analyser);
        }

        [Test]
        public void ShouldReturnFormulaOneDriverAnalyserForValidInput()
        {
            var analyserType = "formulaone";

            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            Assert.IsInstanceOf(typeof(FormulaOneAnalyser), analyser);
        }
        
        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            var analyserType = "";

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

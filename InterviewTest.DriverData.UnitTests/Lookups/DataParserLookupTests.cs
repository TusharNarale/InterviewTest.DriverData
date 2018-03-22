using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Lookups;
using NUnit.Framework;
using System;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class DataParserLookupTests
    {
        [Test]
        public void ShouldReturnJsonParserForValidInput()
        {
            var parserType = "json";
            
            var parser = DataParserLookup.GetParser(parserType);
            Assert.IsInstanceOf(typeof(JsonDataParser), parser);
        }
        
        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            var parserType = "";

            Assert.Throws<ArgumentOutOfRangeException>(() => DataParserLookup.GetParser(parserType));
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => DataParserLookup.GetParser(null));
        }

        [Test]
        public void ShouldThrowExceptionForInvalidInput()
        {
            var parserType = "xml";

            Assert.Throws<ArgumentOutOfRangeException>(() => DataParserLookup.GetParser(parserType));
        }
    }
}

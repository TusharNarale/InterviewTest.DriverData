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
            // Arrange
            var parserType = "json";
            
            // Act
            var parser = DataParserLookup.GetParser(parserType);

            // Assert
            Assert.IsInstanceOf(typeof(JsonDataParser), parser);
        }
        
        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            // Arrange
            var parserType = "";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DataParserLookup.GetParser(parserType));
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            // Arrange
            string parserType = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => DataParserLookup.GetParser(parserType));
        }

        [Test]
        public void ShouldThrowExceptionForInvalidInput()
        {
            // Arrange
            var parserType = "xml";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => DataParserLookup.GetParser(parserType));
        }
    }
}

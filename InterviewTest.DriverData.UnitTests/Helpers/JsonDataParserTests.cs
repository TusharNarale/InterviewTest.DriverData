using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Helpers
{
    [TestFixture]
    public class JsonDataParserTests
    {
        IDataParser _dataParser;

        [SetUp]
        public void Initialize()
        {
            _dataParser = new JsonDataParser();
        }

        [TearDown]
        public void TearDown()
        {
            _dataParser = null;
        }

        [Test]
        public void ShouldParseCorrectly_ForValidJson()
        {
            // Arrange
            var jsonData = "[{\"Start\": \"10/13/2016 12:00:00 AM +00:00\",\"End\": \"10/13/2016 8:54:00 AM +00:00\",\"AverageSpeed\": \"0\"}]";

            // Act
            var actualResult = _dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsInstanceOf(typeof(IReadOnlyCollection<Period>), actualResult);
            Assert.IsTrue(actualResult.Any());
            Assert.AreEqual(0,actualResult.First().AverageSpeed);
        }

        [Test]
        public void ShouldThrowException_ForInvalidJson()
        {
            // Arrange
            var jsonData = "[{\"Start\": \"10/13/2016 12:00:00 AM +00:00\",]";

            // Act and Assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => _dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData));
        }

        [Test]
        public void ShouldReturnNullList_ForEmptyJson()
        {
            // Arrange
            var jsonData = "";

            // Act
            var actualResult = _dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsNull(actualResult);
        }
        
        [Test]
        public void ShouldReturnNullList_ForNullInput()
        {
            // Arrange
            string jsonData = null;
            
            // Act
            var actualResult = _dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}

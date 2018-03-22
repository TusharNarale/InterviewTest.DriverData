using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Lookups;
using NUnit.Framework;
using System;

namespace InterviewTest.DriverData.UnitTests.Lookups
{
    [TestFixture]
    public class DataReaderLookupTests
    {
        [Test]
        public void ShouldReturnFileReaderForValidInput()
        {
            var readerType = "file";
            
            var dataReader = DataReaderLookup.GetReader(readerType);
            Assert.IsInstanceOf(typeof(FileDataReader), dataReader);
        }

        [Test]
        public void ShouldThrowExceptionForEmptyInput()
        {
            var readerType = "";

            Assert.Throws<ArgumentOutOfRangeException>(() => DataReaderLookup.GetReader(readerType));
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => DataReaderLookup.GetReader(null));
        }

        [Test]
        public void ShouldThrowExceptionForInvalidInput()
        {
            var readerType = "apiFeed";

            Assert.Throws<ArgumentOutOfRangeException>(() => DataReaderLookup.GetReader(readerType));
        }
    }
}

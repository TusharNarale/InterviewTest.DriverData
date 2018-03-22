namespace InterviewTest.DriverData.Interfaces
{
    /// <summary>
    /// Provides mechanism for parsing input data into mentioned type
    /// </summary>
    public interface IDataParser
    {
        /// <summary>
        /// Parses input data into expected type
        /// </summary>
        /// <typeparam name="T">Type to which data will be parsed</typeparam>
        /// <param name="data">Input data to parse</param>
        /// <returns>Mentioned type</returns>
        T ParseData<T>(string data);
    }
}
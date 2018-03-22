This document illustrates the project implementation approach.

# Task 1 Analysers
## Task 1.1 Delivery driver implementation, 
 Added DriverConfiguration entity to hold delivery driver configuration that is used to calculate rating and total duration.
 Added PeriodAnalysis entity which contains period duration, rating, flag for 'Is period undocumented', start and end time of period.
 Added DriverConfiguration entity as a readonly parameter to DeliveryDriverAnalyser
 Added 4 basic methods in DeliveryDriverAnalyser for:
	1. Get valid periods
	2. Analyse valid documented periods into list of PeriodAnalysis entities
	3. Analyse undocumented periods into list of PeriodAnalysis entities
	4. Compute final driver rating and analysed duration.
AnalyserLookup is modified to get DeliveryDriverAnalyser instance for type = "delivery". Passed DriverConfiguration instance with predefined values in constructor.
Added unit tests to DeliveryDriverAnalyserTests to check positive as well as negative scenarios

## Task 1.2 Formula One driver implementation
-Implemented the code to get valid periods, analyse documented and undocumented periods into list of PeriodAnalysis instances. Then calculate final rating and duration for the driver.
-Added property RatingForExceedingSpeedLimit to handle ratings in scenario when speed limit is exceeded.
-Added unit tests to test positive as well as negative scenarios.

## Task 1.3 Getaway driver implementation
-Implemented the code to get valid periods, analyse documented and undocumented periods into list of PeriodAnalysis instances. Then calculate final rating and duration for the driver.
-Added property RatingForExceedingSpeedLimit to handle ratings in scenario when speed limit is exceeded.
-Added unit tests to test positive as well as negative scenarios.

## Task 1.4 Penalise Faulty Recording
-Added properties IsPenaltyApplicable and PenaltyForUndocumentedPeriod as a part of configuration. Changed final calculation logic to multiply the final rating by PenaltyForUndocumentedPeriod value if IsPenaltyApplicable flag is true for a driver.

# Task 2
-Used Dictionary with delegates in order to get appropriate analyser based on given input type.

# Task 3
Added Json file for history data. Path to Json file is stored in app.config file
Added IDataReader and IDataParser interfaces to define mechanism for getting data from source and parsing it into ReadOnlyCollection of Periods.
Added FileDataReader to get data from file and read into a string.
Added JsonDataParser to parse input string into a ReadOnlyCollection of Periods.
Added Lookups for both IDataReader and IDataParser.
Wrote unit tests to test analysers for canned data from file.

#Task 4
Wrote tests for Datareader and DataParser lookups.
Tests for analysers were already written during each of the above tasks.

This application rates drivers according to one of several sets of criteria, for the fictional purpose of setting insurance premiums.

# Analysers

The brunt of the work is done by 'Analysers', which process historical driving information into rating information.

## Inputs

- Start time
- End time
- Average speed throughout period

You can assume that:

- Start and end times have single-second granularity (ie, you might see 11:45:21 but never 11:45:21.216)
- In any set of submitted periods:
  - all will belong to the same day
  - none will overlap

In a real-world scenario, the data would likely arrive in series of many short periods - perhaps tens of thousands throughout a day - but the test data in the application is much coarser than this.

## Outputs

All analysers output two pieces of information:

- An overall rating for the driver. The requirements for each analyser tell you how to derive the rating for any given moment. The rating for a particular span of time is the average rating of all moments within it.
- The span of time analysed. The requirements will indicate which blocks of time will be relevant for any given driver.

using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
		// BONUS: What's the difference between DateTime and DateTimeOffset?
        /* DateTime provides date and time which may differ for different timezones.
           DateTimeOffset provides date and time along with an offset indicating difference with UTC date and time. So, in a way it always represents universal time.
        */ 
		public DateTimeOffset Start;
		public DateTimeOffset End;

		// BONUS: What's the difference between decimal and double?
        // double is 64 bit where as decimal is 128 bit. Double performs bit faster than decimal. Decimal is useful where accurate precisions are important.
		public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}

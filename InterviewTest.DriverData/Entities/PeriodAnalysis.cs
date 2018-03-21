using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Entities
{
    public class PeriodAnalysis
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Duration { get; set; }
        public decimal Rating { get; set; }
        public bool IsUndocumented { get; set; }
    }
}

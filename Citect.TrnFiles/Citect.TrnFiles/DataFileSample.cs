using System;
using System.Diagnostics;

namespace Citect.TrnFiles
{
    /// <summary>
    /// DATA Trend File - Sample.
    /// </summary>
    [DebuggerDisplay("{Timestamp} - {Value}")]
    public class DataFileSample
    {
        /// <summary>
        /// The timestamp of the sample.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The value of the sample.
        /// </summary>
        public double Value { get; set; }
    }
}

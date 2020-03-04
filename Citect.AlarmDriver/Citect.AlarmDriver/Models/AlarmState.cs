using System;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect Alarm State object
    /// </summary>
    public class AlarmState
    {
        /// <summary>
        /// The id of the alarm.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the alarm tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The state of the alarm.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// The timestamp of the last acknowledgement.
        /// </summary>
        public DateTime AckTime { get => ackTime; set => ackTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

        /// <summary>
        /// The timestamp of the last Off to On transition.
        /// </summary>
        public DateTime OnTime { get => onTime; set => onTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
       
        /// <summary>
        /// The timestamp of the last On to Off transition.
        /// </summary>
        public DateTime OffTime { get => offTime; set => offTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        
        /// <summary>
        /// The timestamp of the last Enabled to Disabled transition.
        /// </summary>
        public DateTime DisableTime { get => disableTime; set => disableTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }

        private DateTime ackTime;
        private DateTime onTime;
        private DateTime offTime;
        private DateTime disableTime;
    }
}

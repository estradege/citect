using System;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect Event object
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The id of the alarm who raised the event.
        /// </summary>
        public int AlarmId { get; set; }

        /// <summary>
        /// The timestamp of the record.
        /// </summary>
        public DateTime ReordTime { get => recordTime; set => recordTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        private DateTime recordTime;

        /// <summary>
        /// The state of the alarm who raised the event.
        /// </summary>
        public string AlarmStateDesc { get; set; }

        /// <summary>
        /// The event message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The event category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The user who raised the event.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The name of the client (device) where the event was raised.
        /// </summary>
        public string ClientName { get; set; }
    }
}

using System;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect Alarm object
    /// </summary>
    public class Alarm
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
        /// A meaningful description of the alarm.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A meaningful description of the alarm condition.
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// The alarm category to which the alarm is assigned.
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// The name of the graphics page that displays when the AlarmHelp() function is called by a user-defined command.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Any useful comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom1 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom2 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom3 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom4 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom5 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom6 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom7 { get; set; }

        /// <summary>
        /// A user-defined string.
        /// </summary>
        public string Custom8 { get; set; }

        /// <summary>
        /// This field enables you to automatically historize and publish the specified digital alarm in Schneider Electric's Historian application.
        /// </summary>
        public bool Historian { get; set; }

        /// <summary>
        /// The name of the equipment associated with the alarm.
        /// </summary>
        public string Equip { get; set; }

        /// <summary>
        /// The name of the item with which the alarm is associated.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// The state of the alarm.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// The timestamp of the last acknowledgement.
        /// </summary>
        public DateTime AckTime 
        {
            get => ackTime; 
            set => ackTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }
        private DateTime ackTime;

        /// <summary>
        /// The timestamp of the last Off to On transition.
        /// </summary>
        public DateTime OnTime
        {
            get => onTime; 
            set => onTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }
        private DateTime onTime;

        /// <summary>
        /// The timestamp of the last On to Off transition.
        /// </summary>
        public DateTime OffTime 
        {
            get => offTime; 
            set => offTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime offTime;

        /// <summary>
        /// The timestamp of the last Enabled to Disabled transition.
        /// </summary>
        public DateTime DisableTime
        {
            get => disableTime; 
            set => disableTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }
        private DateTime disableTime;

        /// <summary>
        /// The timestamp of the last alarm update.
        /// </summary>
        public DateTime UpdateTime
        {
            get => updateTime;
            set => updateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime updateTime;

        /// <summary>
        /// The timestamp of the last alarm config update.
        /// </summary>
        public DateTime ConfigTime 
        {
            get => configTime;
            set => configTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime configTime;
    }
}

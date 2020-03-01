using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Citect.CtApi
{
    /// <summary>
    /// Extension methods for alarms management
    /// </summary>
    public static class CtApiAlarmEx
    {
        /// <summary>
        /// Acknowledge a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmAckTag(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = ctApi.Cicode($"AlarmAckTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Acknowledge a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmAckTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmAckTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Disables alarms by tag name.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmDisableTag(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = ctApi.Cicode($"AlarmDisableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Disables alarms by tag name.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmDisableTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmDisableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Enables alarms by tag name.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmEnableTag(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = ctApi.Cicode($"AlarmEnableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Enables alarms by tag name.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmEnableTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmEnableTag({tag}, {clusterName})");
            return result;
        }
    }
}

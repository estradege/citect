using System.Collections.Generic;
using System.Threading.Tasks;

namespace Citect
{
    /// <summary>
    /// Extension methods wrapping cicode alarm functions
    /// </summary>
    public static class CtApiAlarmEx
    {
        /// <summary>
        /// Acknowledge a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to acknowledge</param>
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
        /// <param name="tag">A string that identifies the alarm to acknowledge</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmAckTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmAckTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Acknowledge a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarms to acknowledge</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmAckTags(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = ctApi.Cicode($"AlarmAckTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Acknowledge a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarms to acknowledge</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmAckTagsAsync(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = await ctApi.CicodeAsync($"AlarmAckTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Disable a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to disable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmDisableTag(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = ctApi.Cicode($"AlarmDisableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Disable a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to disable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmDisableTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmDisableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Disable a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarm to disable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmDisableTags(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = ctApi.Cicode($"AlarmDisableTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Disable a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarm to disable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmDisableTagsAsync(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = await ctApi.CicodeAsync($"AlarmDisableTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Enable a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to enable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmEnableTag(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = ctApi.Cicode($"AlarmEnableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Enable a specified alarm.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tag">A string that identifies the alarm to enable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmEnableTagAsync(this CtApi ctApi, string tag, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"AlarmEnableTag({tag}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Enable a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarm to enable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string AlarmEnableTags(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = ctApi.Cicode($"AlarmEnableTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }

        /// <summary>
        /// Enable a specified alarms list.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="tags">A list that identifies the alarm to enable</param>
        /// <param name="clusterName">The cluster where the tag resides</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> AlarmEnableTagsAsync(this CtApi ctApi, IEnumerable<string> tags, string clusterName = "")
        {
            var result = "0";

            foreach (var tag in tags)
            {
                var cicodeResult = await ctApi.CicodeAsync($"AlarmEnableTag({tag}, {clusterName})");
                if (cicodeResult != "0")
                {
                    result = cicodeResult;
                }
            }

            return result;
        }
    }
}

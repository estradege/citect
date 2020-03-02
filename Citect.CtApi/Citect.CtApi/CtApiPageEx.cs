using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Citect.CtApi
{
    /// <summary>
    /// Extension methods wrapping cicode page functions
    /// </summary>
    public static class CtApiPageEx
    {
        /// <summary>
        /// Displays a graphics page.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="page">The name or page number of the page to display.</param>
        /// <param name="clusterName">The name of the cluster that will accommodate the page at runtime.</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string PageDisplay(this CtApi ctApi, string page, string clusterName = "")
        {
            var result = ctApi.Cicode($"PageDisplay({page}, {clusterName})");
            return result;
        }

        /// <summary>
        /// Displays a graphics page.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="page">The name or page number of the page to display.</param>
        /// <param name="clusterName">The name of the cluster that will accommodate the page at runtime.</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> PageDisplayAsync(this CtApi ctApi, string page, string clusterName = "")
        {
            var result = await ctApi.CicodeAsync($"PageDisplay({page}, {clusterName})");
            return result;
        }
    }
}

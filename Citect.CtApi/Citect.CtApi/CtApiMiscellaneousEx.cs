using System.Threading.Tasks;

namespace Citect
{
    /// <summary>
    /// Extension methods wrapping cicode miscellaneous functions
    /// </summary>
    public static class CtApiMiscellaneousEx
    {
        /// <summary>
        /// Ends Citect SCADA's operation
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="dest">The destination computer(s) on which Citect SCADA will be shut down.</param>
        /// <param name="project">The full path of the project to run on restart as a string.</param>
        /// <param name="mode">The type of shutdown.</param>
        /// <param name="clusterName">The name of the cluster to which the machine(s) named in Dest belong.</param>
        /// <param name="callEvent">Flag for initiating a user-specified shutdown event prior to shutting down.</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static string Shutdown(this CtApi ctApi, string dest = "", string project = "", int mode = 1, string clusterName = "", int callEvent = 0)
        {
            var result = ctApi.Cicode($"Shutdown(\"{dest}\", \"{project}\", {mode}, \"{clusterName}\", {callEvent})");
            return result;
        }

        /// <summary>
        /// Ends Citect SCADA's operation
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="dest">The destination computer(s) on which Citect SCADA will be shut down.</param>
        /// <param name="project">The full path of the project to run on restart as a string.</param>
        /// <param name="mode">The type of shutdown.</param>
        /// <param name="clusterName">The name of the cluster to which the machine(s) named in Dest belong.</param>
        /// <param name="callEvent">Flag for initiating a user-specified shutdown event prior to shutting down.</param>
        /// <returns>0 (zero) if successful, otherwise an error code will return</returns>
        public static async Task<string> ShutdownAsync(this CtApi ctApi, string dest = "", string project = "", int mode = 1, string clusterName = "", int callEvent = 0)
        {
            var result = await ctApi.CicodeAsync($"Shutdown({dest}, {project}, {mode}, {clusterName}, {callEvent})");
            return result;
        }
    }
}

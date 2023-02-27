using System;
using System.Threading.Tasks;

namespace Citect
{
    /// <summary>
    /// Extension methods wrapping cicode security functions
    /// </summary>
    public static class CtApiSecutityEx
    {
        /// <summary>
        /// Checks the privilege and area of the current operator.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="priv">The privilege level (1..8).</param>
        /// <param name="area">The area of privilege (0..255).</param>
        /// <returns></returns>
        public static bool GetPriv(this CtApi ctApi, int priv, int area)
        {
            var result = ctApi.Cicode($"GetPriv({priv}, {area})");
            return result == "1";
        }

        /// <summary>
        /// Checks the privilege and area of the current operator.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="priv">The privilege level (1..8).</param>
        /// <param name="area">The area of privilege (0..255).</param>
        /// <returns></returns>
        public static async Task<bool> GetPrivAsync(this CtApi ctApi, int priv, int area)
        {
            var result = await ctApi.CicodeAsync($"GetPriv({priv}, {area})");
            return result == "1";
        }

        /// <summary>
        /// Logs an operator into the Plant SCADA system. Not available when logged in as Windows user.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="userName">The user's name, as defined in the Users database.</param>
        /// <param name="password">The user's password, as defined in the Users database.</param>
        /// <param name="sync">Specifies whether the function operates in blocking mode.</param>
        /// <param name="language">The user's language.</param>
        /// <returns>0 (zero) if successful</returns>
        public static string Login(this CtApi ctApi, string userName, string password, bool sync = false, string language = "")
        {
            var result = ctApi.Cicode($"Login(\"{userName}\", \"{password}\", {Convert.ToInt16(sync)}, \"{language}\")");
            return result;
        }

        /// <summary>
        /// Logs an operator into the Plant SCADA system. Not available when logged in as Windows user.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="userName">The user's name, as defined in the Users database.</param>
        /// <param name="password">The user's password, as defined in the Users database.</param>
        /// <param name="sync">Specifies whether the function operates in blocking mode.</param>
        /// <param name="language">The user's language.</param>
        /// <returns>0 (zero) if successful</returns>
        public static async Task<string> LoginAsync(this CtApi ctApi, string userName, string password, bool sync = false, string language = "")
        {
            var result = await ctApi.CicodeAsync($"Login(\"{userName}\", \"{password}\", {Convert.ToInt16(sync)}, \"{language}\")");
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="type">The type of user information.</param>
        /// <returns></returns>
        public static string UserInfo(this CtApi ctApi, int type)
        {
            var result = ctApi.Cicode($"UserInfo({type})");
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <param name="type">The type of user information.</param>
        /// <returns></returns>
        public static async Task<string> UserInfoAsync(this CtApi ctApi, int type)
        {
            var result = await ctApi.CicodeAsync($"UserInfo({type})");
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static bool UserInfoIsLogin(this CtApi ctApi)
        {
            var result = ctApi.UserInfo(0);
            return result == "1";
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static async Task<bool> UserInfoIsLoginAsync(this CtApi ctApi)
        {
            var result = await ctApi.UserInfoAsync(0);
            return result == "1";
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static string UserInfoGetName(this CtApi ctApi)
        {
            var result = ctApi.UserInfo(1);
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static async Task<string> UserInfoGetNameAsync(this CtApi ctApi)
        {
            var result = await ctApi.UserInfoAsync(1);
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static string UserInfoGetFullName(this CtApi ctApi)
        {
            var result = ctApi.UserInfo(2);
            return result;
        }

        /// <summary>
        /// Gets information about the operator who is currently logged-in to the system.
        /// </summary>
        /// <param name="ctApi"></param>
        /// <returns></returns>
        public static async Task<string> UserInfoGetFullNameAsync(this CtApi ctApi)
        {
            var result = await ctApi.UserInfoAsync(2);
            return result;
        }
    }
}

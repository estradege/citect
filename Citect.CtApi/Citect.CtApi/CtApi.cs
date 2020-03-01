using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Citect.CtApi
{
    /// <summary>
    /// Citect ctapi wrapper
    /// </summary>
    public class CtApi : IDisposable
    {
        /// <summary>
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctClose", SetLastError = true)]
        private static extern bool CtClose(IntPtr hCTAPI);

        /// <summary>
        /// Opens a connection to the Citect SCADA API.
        /// </summary>
        /// <param name="sComputer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name.</param>
        /// <param name="sUser">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="sPassword">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <param name="nMode">The mode of the Cicode call. Set this to 0 (zero).</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctOpen", SetLastError = true)]
        private static extern IntPtr CtOpen(string sComputer, string sUser, string sPassword, uint nMode);

        /// <summary>
        /// Reads the current value from the given I/O Device variable tag.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="sTag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <param name="sValue">The buffer to store the read data. The data is returned in string format.</param>
        /// <param name="dwLength">The length of the read buffer. If the data is bigger than the dwLength, the function will not succeed.</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctTagRead", SetLastError = true)]
        private static extern bool CtTagRead(IntPtr hCTAPI, string sTag, StringBuilder sValue, int dwLength);
        
        /// <summary>
        /// Writes the given value to the I/O Device variable tag.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="sTag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <param name="sValue">The value to write to the tag as a string.</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctTagWrite", SetLastError = true)]
        private static extern bool CtTagWrite(IntPtr hCTAPI, string sTag, string sValue);

        /// <summary>
        /// Handle of ctapi connection
        /// </summary>
        private IntPtr _hCtapi = IntPtr.Zero;

        /// <summary>
        /// Looging service
        /// </summary>
        private readonly ILogger<CtApi> _logger;

        /// <summary>
        /// Create a new Citect ctapi wrapper
        /// </summary>
        public CtApi(ILogger<CtApi> logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dispose the Citect ctapi wrapper and close the connection
        /// </summary>
        public void Dispose()
        {
            if (_hCtapi != IntPtr.Zero)
            {
                Close();
            }
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="computer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name. The Windows Computer Name is the name as specified in the Identification tab, under the Network section of the Windows Control Panel.</param>
        /// <param name="user">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="password">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <param name="mode">The mode of the Cicode call. Set this to 0 (zero).</param>
        /// <exception cref="Win32Exception"></exception>
        public void Open(string computer, string user, string password, uint mode = 0)
        {
            if (_hCtapi != IntPtr.Zero)
            {
                Close();
            }
            
            _logger?.LogInformation($"Open a new connection: computer={computer}, user={user}, mode={mode}");           
            _hCtapi = CtOpen(computer, user, password, mode);
            
            if (_hCtapi == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError(error, "CtOpen");
                throw error;
            }

            _logger.LogDebug($"Connection is opened");
        }

        /// <summary>
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public void Close()
        {
            _logger?.LogInformation($"Close the connection");

            var result = CtClose(_hCtapi);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError(error, "CtClose");
                throw error;
            }

            _logger.LogDebug($"Connection is closed");
        }

        /// <summary>
        /// Reads the current value from the given I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <exception cref="Win32Exception"></exception>
        public string TagRead(string tag)
        {
            _logger?.LogInformation($"Read a tag: tag={tag}");

            var value = new StringBuilder(25);
            var result = CtTagRead(_hCtapi, tag, value, value.Capacity);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError(error, "CtTagRead");
                throw error;
            }
            else
            {
                _logger?.LogInformation($"Read a tag: tag={tag}, value={value.ToString()}");
                return value.ToString();
            }
        }

        /// <summary>
        /// Writes the given value to the I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <param name="value">The value to write to the tag as a string.</param>
        /// <exception cref="Win32Exception"></exception>
        public void TagWrite(string tag, string value)
        {
            _logger?.LogInformation($"Write a tag: tag={tag}, value={value}");

            var result = CtTagWrite(_hCtapi, tag, value);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError(error, "CtTagWrite");
                throw error;
            }

            _logger.LogDebug($"Tag is written");
        }
    }
}

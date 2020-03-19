using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Citect
{
    /// <summary>
    /// Citect ctapi wrapper
    /// </summary>
    public class CtApi : IDisposable
    {
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
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctClose", SetLastError = true)]
        private static extern bool CtClose(IntPtr hCTAPI);

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
        /// Executes a Cicode function.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="sCmd">The command to execute.</param>
        /// <param name="hWin">The Citect SCADA window to execute the function. This is a logical Citect SCADA window (0, 1, 2, 3 etc.) not a Windows Handle.</param>
        /// <param name="nMode">The mode of the Cicode call. Set this to 0 (zero).</param>
        /// <param name="sResult">The buffer to put the result of the function call, which is returned as a string. This may be NULL if you do not require the result of the function.</param>
        /// <param name="dwLength">The length of the sResult buffer. If the result of the Cicode function is longer than the this number, then the result is not returned and the function call does not succeed, however the Cicode function is still executed. If the sResult is NULL then this length needs to be 0.</param>
        /// <param name="pctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctCicode", SetLastError = true)]
        private static extern uint CtCicode(IntPtr hCTAPI, string sCmd, uint hWin, uint nMode, StringBuilder sResult, int dwLength, IntPtr pctOverlapped);

        /// <summary>
        /// Searches for the first object in the specified database which satisfies the filter string specified by cluster.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="szTableName">The table, device, trend, or alarm data to be searched.</param>
        /// <param name="szFilter">Filter criteria.</param>
        /// <param name="szCluster">Specifies on which cluster the ctFindFirst function will be performed. If left NULL or empty string then the ctFindFirst will be performed on the active cluster if there is only one.</param>
        /// <param name="pObjHnd">The pointer to the found object handle. This is used to retrieve the properties.</param>
        /// <param name="dwFlags">This argument is no longer used, pass in a value of 0 for this argument.</param>
        /// <returns>If the function succeeds, the return value is a search handle used in a subsequent call to ctFindNext() or ctFindClose(). If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError()</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctFindFirstEx", SetLastError = true)]
        private static extern IntPtr CtFindFirstEx(IntPtr hCTAPI, string szTableName, string szFilter, string szCluster, ref IntPtr pObjHnd, int dwFlags);

        /// <summary>
        /// Retrieves the next object in a search initiated by ctFindFirst().
        /// </summary>
        /// <param name="hnd">Handle to the search, as returned by ctFindFirst().</param>
        /// <param name="pObjHnd">The pointer to the found object handle. This is used to retrieve the properties.</param>
        /// <returns>If the function succeeds, the return value is TRUE (1). If the function does not succeed, the return value is FALSE (0). To get extended error information, call GetLastError()</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctFindNext", SetLastError = true)]
        private static extern bool CtFindNext(IntPtr hnd, ref IntPtr pObjHnd);

        /// <summary>
        /// Closes a search initiated by ctFindFirst().
        /// </summary>
        /// <param name="hnd">Handle to the search, as returned by ctFindFirst().</param>
        /// <returns>If the function succeeds, the return value is non-zero. If the function does not succeed, the return value is zero. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctFindClose", SetLastError = true)]
        private static extern bool CtFindClose(IntPtr hnd);

        /// <summary>
        /// Retrieves an object property.
        /// </summary>
        /// <param name="hnd">Handle to the search, as returned by ctFindFirst().</param>
        /// <param name="szName">The name of the property to be retrieved.</param>
        /// <param name="pData">The result buffer to store the read data. The data is raw binary data, no data conversion or scaling is performed. If this buffer is not large enough to receive the data, the data will be truncated, and the function will return false.</param>
        /// <param name="dwBufferLength">Length of result buffer. If the result buffer is not large enough to receive the data, the data will be truncated, and the function will return false.</param>
        /// <param name="dwResultLength">Length of returned result. You can pass NULL if you want to ignore this parameter.</param>
        /// <param name="dwType">The desired return type.</param>
        /// <returns>If the function succeeds, the return value is non-zero. If the function does not succeed, the return value is zero. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctGetProperty", SetLastError = true)]
        private static extern bool CtGetProperty(IntPtr hnd, string szName, StringBuilder pData, uint dwBufferLength, ref UIntPtr dwResultLength, uint dwType);

        /// <summary>
        /// Handle of ctapi connection
        /// </summary>
        private IntPtr hCtapi = IntPtr.Zero;

        /// <summary>
        /// Looging service
        /// </summary>
        private readonly ILogger<CtApi> logger;

        /// <summary>
        /// Create a new Citect ctapi wrapper
        /// </summary>
        public CtApi()
        {
        }

        /// <summary>
        /// Create a new Citect ctapi wrapper
        /// </summary>
        public CtApi(ILogger<CtApi> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Create a new Citect ctapi wrapper
        /// </summary>
        public CtApi(bool open, string computer = "", string user = "", string password = "")
        {
            if (open)
            {
                Open(computer, user, password);
            }
        }

        /// <summary>
        /// Dispose the Citect ctapi wrapper and close the connection
        /// </summary>
        public void Dispose()
        {
            if (hCtapi != IntPtr.Zero)
            {
                Close();
            }
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public Task OpenAsync()
        {
            return Task.Run(() => Open());
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="computer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name. The Windows Computer Name is the name as specified in the Identification tab, under the Network section of the Windows Control Panel.</param>
        /// <param name="user">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="password">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <exception cref="Win32Exception"></exception>
        public Task OpenAsync(string computer, string user, string password)
        {
            return Task.Run(() => Open(computer, user, password));
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public void Open()
        {
            Open(null, null, null);
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="computer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name. The Windows Computer Name is the name as specified in the Identification tab, under the Network section of the Windows Control Panel.</param>
        /// <param name="user">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="password">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <exception cref="Win32Exception"></exception>
        public void Open(string computer, string user, string password)
        {
            if (hCtapi != IntPtr.Zero)
            {
                Close();
            }
            
            logger?.LogInformation($"Open a new connection: computer={computer}, user={user}");           
            hCtapi = CtOpen(computer, user, password, 0);
            
            if (hCtapi == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtOpen");
                throw error;
            }

            logger?.LogDebug($"Connection is opened");
        }

        /// <summary>
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public void Close()
        {
            logger?.LogInformation($"Close the connection");

            var result = CtClose(hCtapi);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtClose");
                throw error;
            }

            logger?.LogDebug($"Connection is closed");
        }

        /// <summary>
        /// Reads the current value from the given I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <exception cref="Win32Exception"></exception>
        public Task<string> TagReadAsync(string tag)
        {
            return Task.Run(() => TagRead(tag));
        }

        /// <summary>
        /// Reads the current value from the given I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <exception cref="Win32Exception"></exception>
        public string TagRead(string tag)
        {
            logger?.LogInformation($"Read a tag: tag={tag}");

            var value = new StringBuilder(100);
            var result = CtTagRead(hCtapi, tag, value, value.Capacity);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtTagRead");
                throw error;
            }
            else
            {
                logger?.LogInformation($"Read a tag: tag={tag}, value={value.ToString()}");
                return value.ToString();
            }
        }

        /// <summary>
        /// Writes the given value to the I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <param name="value">The value to write to the tag as a string.</param>
        /// <exception cref="Win32Exception"></exception>
        public Task TagWriteAsync(string tag, string value)
        {
            return Task.Run(() => TagWrite(tag, value));
        }

        /// <summary>
        /// Writes the given value to the I/O Device variable tag.
        /// </summary>
        /// <param name="tag">The tag name or tag name and element name, separated by a dot. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference. You may use the array syntax [] to select an element of an array.</param>
        /// <param name="value">The value to write to the tag as a string.</param>
        /// <exception cref="Win32Exception"></exception>
        public void TagWrite(string tag, string value)
        {
            logger?.LogInformation($"Write a tag: tag={tag}, value={value}");

            var result = CtTagWrite(hCtapi, tag, value);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtTagWrite");
                throw error;
            }

            logger?.LogDebug($"Tag is written");
        }

        /// <summary>
        /// Executes a Cicode function.
        /// </summary>
        /// <param name="cmd">The command to execute.</param>
        /// <param name="win">The Citect SCADA window to execute the function. This is a logical Citect SCADA window (0, 1, 2, 3 etc.) not a Windows Handle.</param>
        /// <exception cref="Win32Exception"></exception>
        public Task<string> CicodeAsync(string cmd, uint win = 0)
        {
            return Task.Run(() => Cicode(cmd, win));
        }

        /// <summary>
        /// Executes a Cicode function.
        /// </summary>
        /// <param name="cmd">The command to execute.</param>
        /// <param name="win">The Citect SCADA window to execute the function. This is a logical Citect SCADA window (0, 1, 2, 3 etc.) not a Windows Handle.</param>
        /// <exception cref="Win32Exception"></exception>
        public string Cicode(string cmd, uint win = 0)
        {
            logger?.LogInformation($"Executes a Cicode function: cmd={cmd}, win={win}");

            var value = new StringBuilder(100);
            var result = CtCicode(hCtapi, cmd, win, 0, value, value.Capacity, IntPtr.Zero);
            if (result == 0)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtCicode");
                throw error;
            }
            else
            {
                logger?.LogInformation($"Executes a Cicode function: cmd={cmd}, win={win}, value={value.ToString()}");
                return value.ToString();
            }
        }

        /// <summary>
        /// Searches objects in the specified database which satisfies the filter string specified by cluster.
        /// </summary>
        /// <param name="tableName">The table, device, trend, or alarm data to be searched.</param>
        /// <param name="filter">Filter criteria.</param>
        /// <param name="cluster">Specifies on which cluster the Find function will be performed. If left NULL or empty string then the Find will be performed on the active cluster if there is only one.</param>
        /// <param name="propertiesName">The name of the properties to be retrieved.</param>
        public Task<IEnumerable<Dictionary<string, string>>> FindAsync(string tableName, string filter, string cluster, params string[] propertiesName)
        {
            return Task.Run(() => Find(tableName, filter, cluster, propertiesName));
        }

        /// <summary>
        /// Searches objects in the specified database which satisfies the filter string specified by cluster.
        /// </summary>
        /// <param name="tableName">The table, device, trend, or alarm data to be searched.</param>
        /// <param name="filter">Filter criteria.</param>
        /// <param name="cluster">Specifies on which cluster the Find function will be performed. If left NULL or empty string then the Find will be performed on the active cluster if there is only one.</param>
        /// <param name="propertiesName">The name of the properties to be retrieved.</param>
        public IEnumerable<Dictionary<string, string>> Find(string tableName, string filter, string cluster, params string[] propertiesName)
        {
            logger?.LogInformation($"Searches objects: tableName={tableName}, filter={filter}, cluster={cluster}, propertiesName={string.Join("|", propertiesName)}");

            var hfindptr = IntPtr.Zero;
            var hfind = CtFindFirstEx(hCtapi, tableName, filter, cluster, ref hfindptr, 0);
            if (hfind == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtFindFirstEx");
                throw error;
            }

            var items = new List<Dictionary<string, string>>();
            do
            {
                var item = new Dictionary<string, string>();
                foreach (var propertyName in propertiesName)
                {
                    item[propertyName] = GetProperty(hfindptr, propertyName);
                }

                items.Add(item);
            } while (CtFindNext(hfind, ref hfindptr));            
            CtFindClose(hfind);

            logger?.LogInformation($"Searches objects: tableName={tableName}, filter={filter}, cluster={cluster}, propertiesName={string.Join("|", propertiesName)}, objects.Count={items.Count}");

            return items;
        }

        /// <summary>
        /// Retrieves an object property.
        /// </summary>
        /// <param name="hfindptr">Handle to the search, as returned by ctFindFirst().</param>
        /// <param name="propertyName">The name of the property to be retrieved.</param>
        /// <returns>The property value.</returns>
        private string GetProperty(IntPtr hfindptr, string propertyName)
        {
            logger?.LogDebug($"Get a property: propertyName={propertyName}");

            var pData = new StringBuilder(100);
            var dwResultLength = UIntPtr.Zero;
            var result = CtGetProperty(hfindptr, propertyName, pData, (uint)pData.Capacity, ref dwResultLength, 129);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                logger?.LogError(error, "CtGetProperty");
                return null;
            }
            else
            {
                logger?.LogDebug($"Get a property: propertyName={propertyName}, propertyValue={pData.ToString()}");
                return pData.ToString();
            }
        }
    }
}

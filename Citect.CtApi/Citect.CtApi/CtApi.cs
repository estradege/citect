using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <param name="hCTAPI">The handle to the CTAPI as returned from ctOpen().</param>
        /// <returns>TRUE if successful, otherwise FALSE. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctClose", SetLastError = true)]
        private static extern bool CtClose(IntPtr hCTAPI);

        /// <summary>
        /// Closes a search initiated by ctFindFirst().
        /// </summary>
        /// <param name="hnd">Handle to the search, as returned by ctFindFirst().</param>
        /// <returns>If the function succeeds, the return value is non-zero. If the function does not succeed, the return value is zero. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctFindClose", SetLastError = true)]
        private static extern bool CtFindClose(IntPtr hnd);
        
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

        /// <summary>Adds a tag to the list.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListAdd", SetLastError = true)]
        private static extern IntPtr CtListAdd(IntPtr hList, string sTag);

        /// <summary>Adds a tag to the list with a specified poll period.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <param name="bRaw">Specifies whether to subscribe to the given tag in the list using raw mode if TRUE or engineering mode if FALSE.</param>
        /// <param name="nPollPeriodMS">Dictates the poll period used in the subscription made for the tag (in milliseconds).</param>
        /// <param name="dDeadband">Percentage of the variable tag's engineering range that a tag needs to change by in order for an update to be sent through the system. A value of -1.0 indicates that the default deadband specified by the tag definition is to be used.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListAddEx", SetLastError = true)]
        private static extern IntPtr CtListAddEx(IntPtr hList, string sTag, bool bRaw, int nPollPeriodMS, double dDeadband);

        /// <summary>Gets the value of a tag on the list.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode"> Mode of the data.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListData", SetLastError = true)]
        private static extern bool CtListData(IntPtr hTag, StringBuilder pBuffer, int dwLength, uint dwMode);

        /// <summary>Frees a tag created with ctListAdd().</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListDelete", SetLastError = true)]
        private static extern bool CtListDelete(IntPtr hTag);

        /// <summary>Returns the elements in the list which have changed state since they were last read using the ctListRead() function.</summary>
        /// <param name="hList">The handle to the CTAPI as returned from ctListNew().</param>
        /// <param name="dwMode">The mode of the list event.</param>
        /// <returns>If the function succeeds, the return value specifies a handle to a tag which has changed state since the last time ctListRead was called. If the function does not succeed or there are no changes, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListEvent", SetLastError = true)]
        private static extern IntPtr CtListEvent(IntPtr hList, int dwMode);

        /// <summary>Frees a list created with ctListNew().</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListFree", SetLastError = true)]
        private static extern bool CtListFree(IntPtr hList);

        /// <summary>Gets the tag element item data.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="dwItem">The tag element item.</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode">Mode of the data.</param>    
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListItem", SetLastError = true)]
        private static extern bool CtListItem(IntPtr hTag, int dwItem, StringBuilder pBuffer, int dwLength, uint dwMode);

        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="hnd">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="dwMode">The mode of the list creation.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListNew", SetLastError = true)]
        private static extern IntPtr CtListNew(IntPtr hnd, uint dwMode);

        /// <summary>Reads every tag on the list.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="pctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListRead", SetLastError = true)]
        private static extern bool CtListRead(IntPtr hList, IntPtr pctOverlapped);

        /// <summary>Writes to a single tag on the list.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="sValue">The value to write to the tag as a string. The value will be converted and scaled into the correct format to write to the tag.</param>
        /// <param name="pctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListWrite", SetLastError = true)]
        private static extern bool CtListWrite(IntPtr hTag, string sValue, IntPtr pctOverlapped);

        /// <summary>
        /// Opens a connection to the Citect SCADA API.
        /// </summary>
        /// <param name="sComputer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name.</param>
        /// <param name="sUser">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="sPassword">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <param name="nMode">The mode of the Cicode call.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. Use GetLastError() to get extended error information.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctOpen", SetLastError = true)]
        private static extern IntPtr CtOpen(string sComputer, string sUser, string sPassword, uint nMode);

        /// <summary>
        /// Allows a CTAPI consumer to specify from where it will load certain CTAPI dependencies (.NET managed dependencies).
        /// </summary>
        /// <param name="sPath">A path representing the location of the CTAPI managed dependencies.</param>
        /// <returns></returns>
        [DllImport("CtApi.dll", EntryPoint = "ctSetManagedBinDirectory", SetLastError = true)]
        private static extern bool CtSetManagedBinDirectory(string sPath);

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
        private IntPtr _ctapi = IntPtr.Zero;

        /// <summary>
        /// Looging service
        /// </summary>
        private readonly ILogger<CtApi> _logger;

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
            _logger = logger;
        }

        /// <summary>
        /// Dispose the Citect ctapi wrapper and close the connection
        /// </summary>
        public void Dispose()
        {
            if (_ctapi != IntPtr.Zero)
            {
                Close();
            }
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
            _logger?.LogDebug($"Citect.CtApi > Cicode, cmd={cmd}, win={win}");

            var value = new StringBuilder(100);
            var result = CtCicode(_ctapi, cmd, win, 0, value, value.Capacity, IntPtr.Zero);
            if (result == 0)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > Cicode, cmd={cmd}, win={win}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > Cicode, cmd={cmd}, win={win}, value={value}");
                return value.ToString();
            }
        }

        /// <summary>
        /// Closes a connection to the Citect SCADA API.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public void Close()
        {
            if (_ctapi != IntPtr.Zero)
            {
                _logger?.LogInformation($"Citect.CtApi > Close");

                var result = CtClose(_ctapi);
                if (result == false)
                {
                    var error = new Win32Exception(Marshal.GetLastWin32Error());
                    _logger?.LogError($"Citect.CtApi > Close, error={error.Message}");
                    throw error;
                }

                _ctapi = IntPtr.Zero;
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
            _logger?.LogDebug($"Citect.CtApi > Find, tableName={tableName}, filter={filter}, cluster={cluster}, propertiesName={string.Join("|", propertiesName)}");

            var hfindptr = IntPtr.Zero;
            var hfind = CtFindFirstEx(_ctapi, tableName, filter, cluster, ref hfindptr, 0);
            if (hfind == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > Find, tableName={tableName}, filter={filter}, cluster={cluster}, propertiesName={string.Join("|", propertiesName)}, error={error.Message}");
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

            _logger?.LogDebug($"Citect.CtApi > Find, tableName={tableName}, filter={filter}, cluster={cluster}, propertiesName={string.Join("|", propertiesName)}, objects.Count={items.Count}");

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
            _logger?.LogTrace($"Citect.CtApi > GetProperty, propertyName={propertyName}");

            var pData = new StringBuilder(100);
            var dwResultLength = UIntPtr.Zero;
            var result = CtGetProperty(hfindptr, propertyName, pData, (uint)pData.Capacity, ref dwResultLength, 129);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > GetProperty, propertyName={propertyName}, error={error.Message}");
                return null;
            }
            else
            {
                _logger?.LogTrace($"Citect.CtApi > GetProperty, propertyName={propertyName}, propertyValue={pData}");
                return pData.ToString();
            }
        }

        /// <summary>Adds a tag to the list.</summary>
        /// <param name="list">The handle to the list, as returned from ctListNew().</param>
        /// <param name="tag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <returns>If the function succeeds, the return value specifies a handle.</returns>
        /// <exception cref="Win32Exception"></exception>
        public IntPtr ListAdd(IntPtr list, string tag)
        {
            _logger?.LogDebug($"Citect.CtApi > ListAdd, list={list}, tag={tag}");

            var result = CtListAdd(list, tag);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListAdd list={list}, tag={tag}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListAdd, list={list}, tag={tag}, result={result}");
                return result;
            }
        }

        /// <summary>Adds a tag to the list with a specified poll period.</summary>
        /// <param name="list">The handle to the list, as returned from ctListNew().</param>
        /// <param name="tag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <param name="raw">Specifies whether to subscribe to the given tag in the list using raw mode if TRUE or engineering mode if FALSE.</param>
        /// <param name="pollPeriodMS">Dictates the poll period used in the subscription made for the tag (in milliseconds).</param>
        /// <param name="deadband">Percentage of the variable tag's engineering range that a tag needs to change by in order for an update to be sent through the system. A value of -1.0 indicates that the default deadband specified by the tag definition is to be used.</param>
        /// <returns>If the function succeeds, the return value specifies a handle.</returns>
        /// <exception cref="Win32Exception"></exception>
        public IntPtr ListAddEx(IntPtr list, string tag, bool raw, int pollPeriodMS, double deadband)
        {
            _logger?.LogDebug($"Citect.CtApi > ListAddEx, list={list}, tag={tag}, raw={raw}, pollPeriodMS={pollPeriodMS}, deadband={deadband}");

            var result = CtListAdd(list, tag);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListAddEx list={list}, tag={tag}, raw={raw}, pollPeriodMS={pollPeriodMS}, deadband={deadband}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListAddEx, list={list}, tag={tag}, raw={raw}, pollPeriodMS={pollPeriodMS}, deadband={deadband}, result={result}");
                return result;
            }
        }

        /// <summary>Gets the value of a tag on the list.</summary>
        /// <param name="tag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="mode"> Mode of the data.</param>
        /// <returns>If the function succeeds, the return value specifies the value of the tag on the list.</returns>
        /// <exception cref="Win32Exception"></exception>
        public string ListData(IntPtr tag, uint mode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListData, tag={tag}, mode={mode}");

            var value = new StringBuilder(100);
            var result = CtListData(tag, value, value.Capacity, mode);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListData, tag={tag}, mode={mode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListData, tag={tag}, mode={mode}, result={result}, value={value}");
                return value.ToString();
            }
        }

        /// <summary>Frees a tag created with ctListAdd().</summary>
        /// <param name="tag">The handle to the tag, as returned from ctListAdd().</param>
        /// <exception cref="Win32Exception"></exception>
        public void ListDelete(IntPtr tag)
        {
            _logger?.LogDebug($"Citect.CtApi > ListDelete, tag={tag}");

            var result = CtListDelete(tag);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListDelete tag={tag}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListDelete, tag={tag}, result={result}");
            }
        }

        /// <summary>Returns the elements in the list which have changed state since they were last read using the ctListRead() function.</summary>
        /// <param name="list">The handle to the CTAPI as returned from ctListNew().</param>
        /// <param name="mode">The mode of the list event.</param>
        /// <returns>If the function succeeds, the return value specifies a handle to a tag which has changed state since the last time ctListRead was called. If the function does not succeed or there are no changes, the return value is NULL.</returns>
        public IntPtr ListEvent(IntPtr list, int mode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListEvent, list={list}");
            var result = CtListEvent(list, mode);
            _logger?.LogDebug($"Citect.CtApi > ListEvent, list={list}, result={result}");

            return result;
        }

        /// <summary>Frees a list created with ctListNew().</summary>
        /// <param name="list">The handle to the list, as returned from ctListNew().</param>
        /// <exception cref="Win32Exception"></exception>
        public void ListFree(IntPtr list)
        {
            _logger?.LogDebug($"Citect.CtApi > ListFree, list={list}");

            var result = CtListFree(list);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListFree list={list}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListFree, list={list}, result={result}");
            }
        }

        /// <summary>Gets the tag element item data.</summary>
        /// <param name="tag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="item">The tag element item.</param>
        /// <param name="mode">Mode of the data.</param>    
        /// <returns>If the function succeeds, the return value is the tag element item data.</returns>
        /// <exception cref="Win32Exception"></exception>
        public string ListItem(IntPtr tag, int item, uint mode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListItem, tag={tag}, item={item}, mode={mode}");

            var value = new StringBuilder(100);
            var result = CtListData(tag, value, value.Capacity, mode);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListItem, tag={tag}, item={item}, mode={mode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListItem, tag={tag}, item={item}, mode={mode}, result={result}, value={value}");
                return value.ToString();
            }
        }

        /// <summary>
        /// Creates a new list.
        /// </summary>
        /// <param name="mode">The mode of the list creation.</param>
        /// <returns>If the function succeeds, the return value specifies a handle.</returns>
        /// <exception cref="Win32Exception"></exception>
        public IntPtr ListNew(uint mode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListNew, mode={mode}");

            var result = CtListNew(_ctapi, mode);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListNew, mode={mode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListNew, mode={mode}, result={result}");
                return result;
            }
        }

        /// <summary>Reads every tag on the list.</summary>
        /// <param name="list">The handle to the list, as returned from ctListNew().</param>
        /// <param name="ctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <exception cref="Win32Exception"></exception>
        public void ListRead(IntPtr list, IntPtr ctOverlapped)
        {
            _logger?.LogDebug($"Citect.CtApi > ListRead, list={list}, ctOverlapped={ctOverlapped}");

            var result = CtListRead(list, ctOverlapped);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListRead, list={list}, ctOverlapped={ctOverlapped}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListRead, list={list}, ctOverlapped={ctOverlapped}, result={result}");
            }
        }

        /// <summary>Writes to a single tag on the list.</summary>
        /// <param name="tag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="value">The value to write to the tag as a string. The value will be converted and scaled into the correct format to write to the tag.</param>
        /// <param name="ctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <exception cref="Win32Exception"></exception>
        public void ListWrite(IntPtr tag, string value, IntPtr ctOverlapped)
        {
            _logger?.LogDebug($"Citect.CtApi > ListWrite, tag={tag}, value={value}");

            var result = CtListWrite(tag, value, ctOverlapped);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListWrite tag={tag}, value={value}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListWrite, tag={tag}, value={value}, result={result}");
                return;
            }
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="computer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name. The Windows Computer Name is the name as specified in the Identification tab, under the Network section of the Windows Control Panel.</param>
        /// <param name="user">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="password">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <param name="mode">The mode of the Cicode call.</param>
        /// <exception cref="Win32Exception"></exception>
        public Task OpenAsync(string computer = null, string user = null, string password = null, CtOpen? mode = null)
        {
            return Task.Run(() => Open(computer, user, password, mode));
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="computer">The computer you want to communicate with via CTAPI. For a local connection, specify NULL as the computer name. The Windows Computer Name is the name as specified in the Identification tab, under the Network section of the Windows Control Panel.</param>
        /// <param name="user">Your username as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. On a local computer, it is optional.</param>
        /// <param name="password">Your password as defined in the Citect SCADA project running on the computer you want to connect to. This argument is only necessary if you are calling this function from a remote computer. You need to use a non-blank password. On a local computer, it is optional.</param>
        /// <param name="mode">The mode of the Cicode call.</param>
        /// <exception cref="Win32Exception"></exception>
        public void Open(string computer = null, string user = null, string password = null, CtOpen? mode = null)
        {
            if (_ctapi != IntPtr.Zero)
            {
                Close();
            }

            _logger?.LogInformation($"Citect.CtApi > Open, computer={computer}, user={user}");
            _ctapi = CtOpen(computer, user, password, (uint)(mode ?? 0));

            if (_ctapi == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > Open, computer={computer}, user={user}, error={error.Message}");
                throw error;
            }
        }

        /// <summary>
        /// Allows a CTAPI consumer to specify from where it will load all CTAPI dependencies (Include .NET managed dependencies).
        /// </summary>
        /// <param name="path">A path representing the location of the CTAPI managed dependencies.</param>
        /// <returns></returns>
        public void SetCtApiDirectory(string path)
        {
            _logger?.LogInformation($"Citect.CtApi > SetCtApiDirectory, path={path}");

            var envPath = (string)Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process)["Path"];
            var envPathArray = envPath?.Split(';') ?? Array.Empty<string>();
            if (!envPathArray.Contains(path))
            {
                Environment.SetEnvironmentVariable("Path", $"{envPath};{path}", EnvironmentVariableTarget.Process);
                SetManagedBinDirectory(path);
            }
        }

        /// <summary>
        /// Allows a CTAPI consumer to specify from where it will load certain CTAPI dependencies (.NET managed dependencies).
        /// </summary>
        private void SetManagedBinDirectory(string path)
        {
            try
            {
                _logger?.LogInformation($"Citect.CtApi > SetManagedBinDirectory, path={path}");
                var result = CtSetManagedBinDirectory(path);
                _logger?.LogInformation($"Citect.CtApi > SetManagedBinDirectory, path={path}, result={result}");
            }
            catch (Exception e)
            {
                _logger?.LogWarning($"Citect.CtApi > SetManagedBinDirectory, error={e.Message}");
                _logger?.LogTrace($"Citect.CtApi > SetManagedBinDirectory, error={e}");
            }
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
            _logger?.LogDebug($"Citect.CtApi > TagRead, tag={tag}");

            var value = new StringBuilder(100);
            var result = CtTagRead(_ctapi, tag, value, value.Capacity);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > TagRead, tag={tag}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > TagRead, tag={tag}, value={value}");
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
            _logger?.LogDebug($"Citect.CtApi > TagWrite, tag={tag}, value={value}");

            var result = CtTagWrite(_ctapi, tag, value);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogDebug($"Citect.CtApi > TagWrite, tag={tag}, value={value}, error={error.Message}");
                throw error;
            }
        }
    }
}

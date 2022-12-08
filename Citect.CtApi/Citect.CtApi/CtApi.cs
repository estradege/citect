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
        /// Allows a CTAPI consumer to specify from where it will load certain CTAPI dependencies (.NET managed dependencies).
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        [DllImport("CtApi.dll", EntryPoint = "ctSetManagedBinDirectory", SetLastError = true)]
        private static extern bool ctSetManagedBinDirectory(string sPath);

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
        /// Creates a new list.
        /// </summary>
        /// <param name="hnd">The handle to the CTAPI as returned from ctOpen().</param>
        /// <param name="dwMode">The mode of the list creation. The following modes are supported:
        ///   CT_LIST_EVENT (1) - Creates the list in event mode. This mode allows you to use the ctListEvent() function. 
        ///   CT_LIST_LIGHTWEIGHT_MODE (2) - Setting this mode for a list means any tag updates will use a "lightweight" version of the tag value that 
        ///   does not include a quality timestamp or value timestamp. 
        ///   These flags can be used in combination with each other.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListNew", SetLastError = true)]
        private static extern IntPtr CtListNew(IntPtr hnd, uint dwMode);

        /// <summary>Frees a list created with ctListNew. Every tag added to the list is freed, you do not have to call ctListDelete() for each tag. not call ctListFree() while a read operation is pending. Wait for the read to complete before freeing the list.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListFree", SetLastError = true)]
        private static extern bool CtListFree(IntPtr hList);

        /// <summary>Adds a tag or tag element to the list. Once the tag has been added to the list, it may be read using ctListRead() and written to using ctListWrite(). If a read is already pending, the tag will not be read until the next time ctListRead() is called. ctListWrite() may be called immediately after the ctListAdd() function has completed.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListAdd", SetLastError = true)]
        private static extern IntPtr CtListAdd(IntPtr hList, string sTag);

        /// <summary>Returns the elements in the list which have changed state since they were last read using the ctListRead() function. You need to have created the list with CT_LIST_EVENT mode in the ctListNew() function.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="dwMode">The mode of the list event. You need to use the same mode for each call to ctListEvent() until NULL is returned before changing mode. The following modes are supported:
        ///  CT_LIST_EVENT_NEW (1) - Gets notifications when tags are added to the list. When this mode is used, you will get an event message when new tags added to the list. 
        ///  CT_LIST_EVENT_STATUS (2) - Gets notifications for status changes. Tags will change status when the I/O Device goes offline. When this mode is used, you will get a notification when the tag goes into #COM and another one when it goes out of #COM. You can verify that the tag is in #COM when an error is returned from ctListData() for that tag.</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListEvent", SetLastError = true)]
        private static extern IntPtr CtListEvent(IntPtr hList, int dwMode);

        /// <summary>Adds a tag or tag element to the list. Once the tag has been added to the list, it may be read using ctListRead() and written to using ctListWrite(). If a read is already pending, the tag will not be read until the next time ctListRead() is called. ctListWrite() may be called immediately after the ctListAdd() function has completed.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <param name="bRaw">Specifies whether to subscribe to the given tag in the list using raw mode if TRUE or engineering mode if FALSE.</param>
        /// <param name="nPollPeriodMS">Dictates the poll period used in the subscription made for the tag (in milliseconds).</param>
        /// <param name="dDeadband">Percentage of the variable tag's engineering range that a tag needs to change by in order for an update to be sent through the system. A value of -1.0 indicates that the default deadband specified by the tag definition is to be used. </param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListAddEx", SetLastError = true)]
        private static extern IntPtr CtListAddEx(IntPtr hList, string sTag, bool bRaw, int nPollPeriodMS, double dDeadband);

        /// <summary>Reads the tags on the list. This function will read tags which are attached to the list. Once the data has been read from the I/O Devices, you may call ctListData()to get the values of the tags. If the read does not succeed, ctListData() will return an error for the tags that cannot be read.</summary>
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

        /// <summary>Gets the value of a tag on the list. Call this function after ctListRead() has completed for the added tag. You may call ctListData() while subsequent ctListRead() functions are pending, and the last data read will be returned. If you wish to get the value of a specific quality part of a tag element item data use ctListItem which includes the same parameters with the addition of the dwItem parameter.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode">Mode of the data. The following modes are supported:
        ///  0 (zero) - The value is scaled using the scale specified in the CitectSCADA project, and formatted using the format specified in the CitectSCADA project. 
        /// FMT_NO_FORMAT (2) - The value is not formatted to the format specified in the CitectSCADA project. A default format is used. If there is a scale specified in the CitectSCADA project, it will be used to scale the value. 
        /// The dwMode argument no longer supports option FMT_NO_SCALE which allowed you to dynamically get the raw value or the engineering value of a tag in the list. If you wish to get the raw value of a tag, add it to the list with this mode by calling ctListAddEx and specifying bRaw = TRUE.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListData", SetLastError = true)]
        private static extern bool CtListData(IntPtr hTag, StringBuilder pBuffer, int dwLength, uint dwMode);

        /// <summary>Gets the tag element item data. For specific quality part values please refer to The Quality Tag Element.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="dwItem">The tag element item:
        ///  CT_LIST_VALUE (0x1) - value.
        ///  CT_LIST_TIMESTAMP (0x2) – timestamp.
        ///  CT_LIST_VALUE_TIMESTAMP (0x3) – value timestamp.
        ///  CT_LIST_QUALITY_TIMESTAMP (0x4) - quality timestamp.
        ///  CT_LIST_QUALITY_GENERAL (0x5) - quality general.
        ///  CT_LIST_QUALITY_SUBSTATUS (0x6) - quality substatus.
        ///  CT_LIST_QUALITY_LIMIT (0x7) - quality limit .
        ///  CT_LIST_QUALITY_EXTENDED_SUBSTATUS (0x8) - quality extended substatus.
        ///  CT_LIST_QUALITY_DATASOURCE_ERROR (0x9) - quality datasource error.
        ///  CT_LIST_QUALITY_OVERRIDE (0xa) - quality override flag.
        ///  CT_LIST_QUALITY_CONTROL_MODE (0xb) - quality control mode flag.</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode">Mode of the data. The following modes are supported:
        ///  0 (zero) - The value is scaled using the scale specified in the CitectSCADA project, and formatted using the format specified in the CitectSCADA project. 
        ///  FMT_NO_FORMAT (2) - The value is not formatted to the format specified in the CitectSCADA project. A default format is used. If there is a scale specified in the CitectSCADA project, it will be used to scale the value. 
        ///  The dwMode argument no longer supports option FMT_NO_SCALE which allowed you to dynamically get the raw value or the engineering value of a tag in the list. If you wish to get the raw value of a tag, add it to the list with this mode by calling ctListAddEx and specifying bRaw = TRUE.. </param>    
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListItem", SetLastError = true)]
        private static extern bool CtListItem(IntPtr hTag, int dwItem, StringBuilder pBuffer, int dwLength, uint dwMode);

        /// <summary>Frees a tag created with ctListAdd. Your program is permitted to call ctListDelete() while a read or write is pending on another thread. The ctListWrite() and ctListRead() will return once the tag has been deleted.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        [DllImport("CtApi.dll", EntryPoint = "ctListDelete", SetLastError = true)]
        private static extern bool CtListDelete(IntPtr hTag);


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
            if (_ctapi != IntPtr.Zero)
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
            SetManagedBinDirectory();

            if (_ctapi != IntPtr.Zero)
            {
                Close();
            }

            _logger?.LogInformation($"Citect.CtApi > Open, computer={computer}, user={user}");
            _ctapi = CtOpen(computer, user, password, 0);
            
            if (_ctapi == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > Open, computer={computer}, user={user}, error={error.Message}");
                throw error;
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
        /// Creates a new list.
        /// </summary>
        /// <param name="dwMode">The mode of the list creation. The following modes are supported:
        ///   CT_LIST_EVENT (1) - Creates the list in event mode. This mode allows you to use the ctListEvent() function. 
        ///   CT_LIST_LIGHTWEIGHT_MODE (2) - Setting this mode for a list means any tag updates will use a "lightweight" version of the tag value that 
        ///   does not include a quality timestamp or value timestamp. 
        ///   These flags can be used in combination with each other.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed an exception is thrown.</returns>
        public IntPtr ListNew(uint dwMode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListNew, dwMode={dwMode}");

            var result = CtListNew(_ctapi, dwMode);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListNew, dwMode={dwMode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListNew, dwMode={dwMode}, result={result}");
                return result;
            }
        }        

        /// <summary>Frees a list created with ctListNew. Every tag added to the list is freed, you do not have to call ctListDelete() for each tag. not call ctListFree() while a read operation is pending. Wait for the read to complete before freeing the list.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        public void ListFree(IntPtr hList)
        {
            _logger?.LogDebug($"Citect.CtApi > ListFree, hList={hList}");

            var result = CtListFree(hList);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListFree hList={hList}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListFree, hList={hList}, result={result}");
            }
        }


        /// <summary>Frees a tag created with ctListAdd. Your program is permitted to call ctListDelete() while a read or write is pending on another thread. The ctListWrite() and ctListRead() will return once the tag has been deleted.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        public void ListDelete(IntPtr hTag)
        {
            _logger?.LogDebug($"Citect.CtApi > ListDelete, hTag={hTag}");

            var result = CtListDelete(hTag);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListDelete hTag={hTag}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListDelete, hTag={hTag}, result={result}");
            }
        }


        /// <summary>Returns the elements in the list which have changed state since they were last read using the ctListRead() function. You need to have created the list with CT_LIST_EVENT mode in the ctListNew() function.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="dwMode">The mode of the list event. You need to use the same mode for each call to ctListEvent() until NULL is returned before changing mode. The following modes are supported:
        ///  CT_LIST_EVENT_NEW - Gets notifications when tags are added to the list. When this mode is used, you will get an event message when new tags added to the list. 
        ///  CT_LIST_EVENT_STATUS - Gets notifications for status changes. Tags will change status when the I/O Device goes offline. When this mode is used, you will get a notification when the tag goes into #COM and another one when it goes out of #COM. You can verify that the tag is in #COM when an error is returned from ctListData() for that tag.</returns>
        /// <returns>The return value specifies a handle to a tag which has changed state since the last time ctListRead was called. If the function does not succeed or there are no changes, the return value is NULL.</returns>
        public IntPtr ListEvent(IntPtr hList, int dwMode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListEvent, hList={hList}");

            var result = CtListEvent(hList,dwMode);
            _logger?.LogDebug($"Citect.CtApi > ListEvent, hList={hList}, result={result}");
            return result;
        }

        /// <summary>Adds a tag or tag element to the list. Once the tag has been added to the list, it may be read using ctListRead() and written to using ctListWrite(). If a read is already pending, the tag will not be read until the next time ctListRead() is called. ctListWrite() may be called immediately after the ctListAdd() function has completed.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, an exception is thrown.</returns>
        public IntPtr ListAdd(IntPtr hList, string sTag)
        {
            _logger?.LogDebug($"Citect.CtApi > ListAdd, hList={hList}, sTag={sTag}");

            var result = CtListAdd(hList,sTag);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListAdd hList={hList}, sTag={sTag}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListAdd, hList={hList}, sTag={sTag}, result={result}");
                return result;
            }
        }

        /// <summary>Adds a tag or tag element to the list. Once the tag has been added to the list, it may be read using ctListRead() and written to using ctListWrite(). If a read is already pending, the tag will not be read until the next time ctListRead() is called. ctListWrite() may be called immediately after the ctListAdd() function has completed.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="sTag">The tag or tag name and element name, separated by a dot to be added to the list. If the element name is not specified, it will be resolved at runtime as for an unqualified tag reference.</param>
        /// <param name="bRaw">Specifies whether to subscribe to the given tag in the list using raw mode if TRUE or engineering mode if FALSE.</param>
        /// <param name="nPollPeriodMS">Dictates the poll period used in the subscription made for the tag (in milliseconds).</param>
        /// <param name="dDeadband">Percentage of the variable tag's engineering range that a tag needs to change by in order for an update to be sent through the system. A value of -1.0 indicates that the default deadband specified by the tag definition is to be used. </param>
        /// <returns>If the function succeeds, the return value specifies a handle. If the function does not succeed, the return value is NULL. To get extended error information, call GetLastError().</returns>
        public IntPtr ListAddEx(IntPtr hList, string sTag, bool bRaw, int nPollPeriodMS, double dDeadband)
        {
            _logger?.LogDebug($"Citect.CtApi > ListAddEx, hList={hList}, sTag={sTag}, bRaw={bRaw}, nPollPeriodMS={nPollPeriodMS}, dDeadband={dDeadband}");

            var result = CtListAdd(hList,sTag);
            if (result == IntPtr.Zero)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListAddEx hList={hList}, sTag={sTag}, bRaw={bRaw}, nPollPeriodMS={nPollPeriodMS}, dDeadband={dDeadband}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListAddEx, hList={hList}, sTag={sTag}, bRaw={bRaw}, nPollPeriodMS={nPollPeriodMS}, dDeadband={dDeadband}, result={result}");
                return result;
            }
        }


        /// <summary>Writes to a single tag on the list.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="sValue">The value to write to the tag as a string. The value will be converted and scaled into the correct format to write to the tag.</param>
        /// <param name="pctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function does not succeed, the return value is FALSE. To get extended error information, call GetLastError().</returns>
        //[DllImport("CtApi.dll", EntryPoint = "ctListWrite", SetLastError = true)]
        //private static extern bool CtListWrite(IntPtr hTag, string sValue, IntPtr pctOverlapped);
        public void ListWrite(IntPtr hTag, string sValue, IntPtr pctOverlapped)
        {
            _logger?.LogDebug($"Citect.CtApi > ListWrite, hTag={hTag}, sValue={sValue}");

            var result = CtListWrite(hTag,sValue,pctOverlapped);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListWrite hTag={hTag}, sValue={sValue}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListWrite, hTag={hTag}, sValue={sValue}, result={result}");
                return;
            }
        }


        /// <summary>Reads the tags on the list. This function will read tags which are attached to the list. Once the data has been read from the I/O Devices, you may call ctListData()to get the values of the tags. If the read does not succeed, ctListData() will return an error for the tags that cannot be read.</summary>
        /// <param name="hList">The handle to the list, as returned from ctListNew().</param>
        /// <param name="pctOverlapped">CTOVERLAPPED structure. This structure is used to control the overlapped notification. Set to NULL if you want a synchronous function call.</param>
        public void ListRead(IntPtr hList, IntPtr pctOverlapped)
        {
            _logger?.LogDebug($"Citect.CtApi > ListRead, hList={hList}, pctOverlapped={pctOverlapped}");

            var result = CtListRead(hList,pctOverlapped);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListRead, hList={hList}, pctOverlapped={pctOverlapped}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListRead, hList={hList}, pctOverlapped={pctOverlapped}, result={result}");
            }
        }

        /// <summary>Gets the value of a tag on the list. Call this function after ctListRead() has completed for the added tag. You may call ctListData() while subsequent ctListRead() functions are pending, and the last data read will be returned. If you wish to get the value of a specific quality part of a tag element item data use ctListItem which includes the same parameters with the addition of the dwItem parameter.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode">Mode of the data. The following modes are supported:
        ///  0 (zero) - The value is scaled using the scale specified in the CitectSCADA project, and formatted using the format specified in the CitectSCADA project. 
        /// FMT_NO_FORMAT (2) - The value is not formatted to the format specified in the CitectSCADA project. A default format is used. If there is a scale specified in the CitectSCADA project, it will be used to scale the value. 
        /// The dwMode argument no longer supports option FMT_NO_SCALE which allowed you to dynamically get the raw value or the engineering value of a tag in the list. If you wish to get the raw value of a tag, add it to the list with this mode by calling ctListAddEx and specifying bRaw = TRUE.</param>
        /// <returns>If the function succeeds, the return value is the value as string. If the function does not succeed, an exception is thrown.</returns>
        public string ListData(IntPtr hTag, uint dwMode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListData, hTag={hTag}, dwMode={dwMode}");

            var value = new StringBuilder(100);
            var result = CtListData(hTag,value,value.Capacity,dwMode);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListData, hTag={hTag}, dwMode={dwMode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListData, hTag={hTag}, dwMode={dwMode}, result={result}, value={value}");
                return value.ToString();
            }
        }

        /// <summary>Gets the tag element item data. For specific quality part values please refer to The Quality Tag Element.</summary>
        /// <param name="hTag">The handle to the tag, as returned from ctListAdd().</param>
        /// <param name="dwItem">The tag element item:
        ///  CT_LIST_VALUE (0x1) - value.
        ///  CT_LIST_TIMESTAMP (0x2) – timestamp.
        ///  CT_LIST_VALUE_TIMESTAMP (0x3) – value timestamp.
        ///  CT_LIST_QUALITY_TIMESTAMP (0x4) - quality timestamp.
        ///  CT_LIST_QUALITY_GENERAL (0x5) - quality general.
        ///  CT_LIST_QUALITY_SUBSTATUS (0x6) - quality substatus.
        ///  CT_LIST_QUALITY_LIMIT (0x7) - quality limit .
        ///  CT_LIST_QUALITY_EXTENDED_SUBSTATUS (0x8) - quality extended substatus.
        ///  CT_LIST_QUALITY_DATASOURCE_ERROR (0x9) - quality datasource error.
        ///  CT_LIST_QUALITY_OVERRIDE (0xa) - quality override flag.
        ///  CT_LIST_QUALITY_CONTROL_MODE (0xb) - quality control mode flag.</param>
        /// <param name="pBuffer">Pointer to a buffer to return the data. The data is returned scaled and as a formatted string.</param>
        /// <param name="dwLength">Length (in bytes) of the raw data buffer.</param>
        /// <param name="dwMode">Mode of the data. The following modes are supported:
        ///  0 (zero) - The value is scaled using the scale specified in the CitectSCADA project, and formatted using the format specified in the CitectSCADA project. 
        ///  FMT_NO_FORMAT (2) - The value is not formatted to the format specified in the CitectSCADA project. A default format is used. If there is a scale specified in the CitectSCADA project, it will be used to scale the value. 
        ///  The dwMode argument no longer supports option FMT_NO_SCALE which allowed you to dynamically get the raw value or the engineering value of a tag in the list. If you wish to get the raw value of a tag, add it to the list with this mode by calling ctListAddEx and specifying bRaw = TRUE.. </param>    
        /// <returns>If the function succeeds, the return value is the requested data formatted as a string.</returns>
        public string ListItem(IntPtr hTag, int dwItem, uint dwMode)
        {
            _logger?.LogDebug($"Citect.CtApi > ListItem, hTag={hTag}, dwItem={dwItem}, dwMode={dwMode}");

            var value = new StringBuilder(100);
            var result = CtListData(hTag,value,value.Capacity,dwMode);
            if (result == false)
            {
                var error = new Win32Exception(Marshal.GetLastWin32Error());
                _logger?.LogError($"Citect.CtApi > ListItem, hTag={hTag}, dwItem={dwItem}, dwMode={dwMode}, error={error.Message}");
                throw error;
            }
            else
            {
                _logger?.LogDebug($"Citect.CtApi > ListItem, hTag={hTag}, dwItem={dwItem}, dwMode={dwMode}, result={result}, value={value}");
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

        /// <summary>
        /// Allows a CTAPI consumer to specify from where it will load certain CTAPI dependencies (.NET managed dependencies).
        /// </summary>
        private void SetManagedBinDirectory()
        {
            try
            {
                var path = @"C:\ProgramData\CitectCtApi";

                _logger?.LogInformation($"Citect.CtApi > SetManagedBinDirectory, path={path}");
                var result = ctSetManagedBinDirectory(path);
                _logger?.LogInformation($"Citect.CtApi > SetManagedBinDirectory, path={path}, result={result}");
            }
            catch (Exception e)
            {
                _logger?.LogWarning($"Citect.CtApi > SetManagedBinDirectory, error={e.Message}");
                _logger?.LogTrace($"Citect.CtApi > SetManagedBinDirectory, error={e}");
            }
        }
    }
}

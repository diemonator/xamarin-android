using System.Diagnostics;
using System.Reflection;

namespace isflib
{

    public class isflib
    {
        /* Examples of GetVersion():
            isflib.isflib.GetVersion();
            isflib.isflib.GetVersion(typeof(Program).Assembly);
            isflib.isflib.GetVersion(Assembly.GetEntryAssembly());
            isflib.isflib.GetVersion(Assembly.GetCallingAssembly());
            isflib.isflib.GetVersion(Assembly.GetExecutingAssembly());
        */

        /// <summary>
        /// Get version of ISF (full backwards compatibility)
        /// </summary>
        /// <returns>ISF version</returns>
        static public string GetVersion()
        {
            return GetVersion(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Get version of assembly (library or application)
        /// </summary>
        /// <param name="aAssembly">use Assembly.GetExecutingAssembly(), to get version of any assembly (or application)</param>
        /// <returns>Assembly version</returns>
        static public string GetVersion(Assembly aAssembly)
        {
            FileVersionInfo xFileVersionInfo = FileVersionInfo.GetVersionInfo(aAssembly.Location);
            AssemblyName xAssemblyName = aAssembly.GetName();
            return string.Format("{0}, v.{1}",
                xFileVersionInfo.ProductName,
                xAssemblyName.Version.ToString());
        }
    }

}

// EOF


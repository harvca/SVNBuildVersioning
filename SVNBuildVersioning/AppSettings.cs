using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SVNBuildVersioning
{
    public class AppSettings
    {
        public static Version Version(string assemblyName)
        {
            AssemblyName asm = new AssemblyName(assemblyName);
            return asm.Version;
        }
        public static string LogFile;// = string.Format("{0}\\SVNBuildVersioning-{1}.log", AppSettings.LogDIR, DateTime.Now.ToString("yyyyMM"));

        public static string LogDIR
        {
            get
            {
                try
                {
                    string tmp = System.Configuration.ConfigurationManager.AppSettings["LogDIR"];
                    if (string.IsNullOrWhiteSpace(tmp)) { throw new Exception("LogDIR is NULL"); }
                    return tmp;
                }
                catch
                {
                    Log.Add("LogDIR is NULL");
                    throw new Exception("LogDIR is NULL");
                }
            }
        }
        public static string TortoiseSVNPath
        {
            get
            {
                try
                {
                    string tmp = System.Configuration.ConfigurationManager.AppSettings["TortoiseSVNPath"];
                    if (string.IsNullOrWhiteSpace(tmp)) { throw new Exception("TortoiseSVNPath is NULL"); }
                    return tmp;
                }
                catch
                {
                    Log.Add("TortoiseSVNPath is NULL");
                    throw new Exception("TortoiseSVNPath is NULL");
                }
            }
        }
    }
}

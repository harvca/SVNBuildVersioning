using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SVNBuildVersioning
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SVN Build Versioning";
            if (args.Count() == 3)
            {
                string projectPath = args[0];
                string assembly = args[1]; 
                string buildType = args[2];
                AppSettings.LogFile = string.Format("{0}\\SVNBuildVersioning-{1}-{2}.log", AppSettings.LogDIR, assembly, DateTime.Now.ToString("yyyyMM"));
                bool isRelease = buildType.ToLower() == "release" || buildType.ToLower() == "prod" || buildType.ToLower() == "uat" ? true : false;
                string assemblyInfoPath = string.Format("{0}Properties\\AssemblyInfo.cs", projectPath);
                string subWCRevPath = string.Format("{0}\\bin\\SubWCRev.exe", AppSettings.TortoiseSVNPath);
                string lastCommittedRevision = string.Empty;
              

                Log.Add("--------------------------------------------------------------------------------");
                Log.Add("projectPath: " + projectPath);
                Log.Add("assembly: " + assembly);
                Log.Add("buildType: " + buildType);
                Log.Add("isRelease: " + isRelease);
                Log.Add("assemblyInfoPath: " + assemblyInfoPath);
                Log.Add("subWCRevPath: " + subWCRevPath);

                string assemblyInfoData = System.IO.File.ReadAllText(assemblyInfoPath);
                Match rxmVersion = Regex.Match(assemblyInfoData, "([0-9]+)([.][0-9]+[.][0-9]+[.][0-9]+)");

                Version oldAssemblyVersion = new Version(rxmVersion.Value);

                Log.Add("oldAssemblyVersion: " + oldAssemblyVersion);

                System.Diagnostics.ProcessStartInfo SubWCRevProcessStartInfo = new System.Diagnostics.ProcessStartInfo();
                SubWCRevProcessStartInfo.UseShellExecute = false;
                SubWCRevProcessStartInfo.CreateNoWindow = true;
                SubWCRevProcessStartInfo.RedirectStandardOutput = true;
                SubWCRevProcessStartInfo.FileName = subWCRevPath;
                SubWCRevProcessStartInfo.Arguments = projectPath;

                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(SubWCRevProcessStartInfo))
                {
                    using (System.IO.StreamReader sm = process.StandardOutput)
                    {
                        string resRaw = sm.ReadToEnd();
                        string[] res = resRaw.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        lastCommittedRevision = res[1].Replace("Last committed at revision ", "");
                        Log.Add("lastCommittedRevision: " + lastCommittedRevision);
                    }
                }
                int build = oldAssemblyVersion.Build;

                if (isRelease == false) { build = oldAssemblyVersion.Build + 1; }

                Version newAssemblyVersion = Version.Parse(string.Format("{0}.{1}.{2}.{3}", oldAssemblyVersion.Major, oldAssemblyVersion.Minor, build, lastCommittedRevision));
                Log.Add("newAssemblyVersion: " + newAssemblyVersion);

                if (newAssemblyVersion > oldAssemblyVersion)
                {
                    System.IO.File.WriteAllText(assemblyInfoPath, Regex.Replace(assemblyInfoData, "([0-9]+)([.][0-9]+[.][0-9]+[.][0-9]+)", newAssemblyVersion.ToString()));
                }
                Log.Add("--------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Usage: SVNBuildVersioning.exe [Project Directory] [Assembly Name] [Build Type]");
            }

#if DEBUG
            Console.ReadLine();
#endif
            
        }
    }
}

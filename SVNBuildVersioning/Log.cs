using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVNBuildVersioning
{
   public class Log
    {
       public static void Add(string message, string logFile)
       {
           try
           {
               if (System.IO.File.Exists(logFile) == false)
               {
                   System.IO.File.Create(logFile).Close();
               }
               Console.WriteLine(message);
               System.IO.File.AppendAllText(logFile, message + Environment.NewLine);
           }
           catch { }
       }
    }
}

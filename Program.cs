using System;
using System.Diagnostics;
using System.Linq;

namespace VpncScriptWin_VpnSlice_bridge
{
    class Program
    {
    	// wrapper to call python module and pass arguments to it instead of CScript .js / .vbs script
    	// copy the "fake" cscript.exe to the folder with openconnect(-gui)
        // call it like this: openconnect .... -s "C:\projects\vpn-slice-folder|vpn_slice|-vvv --dump --no-fork corporate.company.com"
        static void Main(string[] args)
        {
            // string exeDir = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            // string[] allArgs = Environment.GetCommandLineArgs();
            // string remargs = Environment.CommandLine.Substring(allArgs[0].Length);
            string remargs = args[0];
            var split = remargs.Split('|');
            string pythonModulePath = split[0];
            string pythonModuleName = split[1];
            string xargs = split[2];
            string rargs = "-m " + pythonModuleName + " " + xargs;
            var psi = new ProcessStartInfo("python3", rargs);            
            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;
            psi.Environment.Add("PYTHONPATH", pythonModulePath);
            
            System.Console.Error.WriteLine(
                "executing: " + "python3" + 
                " with PYTHONPATH: " + 
                pythonModulePath + " with args: " + rargs
                );

            var proc = Process.Start(psi);
            proc.WaitForExit();
        }
    }
}

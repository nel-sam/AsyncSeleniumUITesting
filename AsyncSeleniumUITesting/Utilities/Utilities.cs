using System;

namespace AsyncSeleniumUITesting
{
    public static class Utilities
    {
        /// <returns>The process ID if the process was started successfully.</returns>
        public static string RunCmdCommand(string command)
        {
            string output = String.Empty;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            
            while (!process.StandardOutput.EndOfStream)
            {
                 output += process.StandardOutput.ReadLine();
            }

            return output;
        }

        public static void KillProcess(string procName)
        {
            RunCmdCommand("taskkill /f /im " + procName);
        }
    }
}

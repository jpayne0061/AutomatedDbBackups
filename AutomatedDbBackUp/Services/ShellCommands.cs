using System.Diagnostics;

namespace AutomatedDbBackUp.Services
{
    public class ShellCommands
    {
        public string ExecuteShellCommand(string fileCommand, string args)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileCommand,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
    }
}

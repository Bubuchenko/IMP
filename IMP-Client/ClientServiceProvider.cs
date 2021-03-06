﻿using IMP_Lib.Contracts;
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

        public Task<string> CreateFile(string path, string name)
        {
            return Task.FromResult(FileInspector.CreateFile(path, name));
        }

        public Task<string> CreateFolder(string path, string name)
        {
            return Task.FromResult(FileInspector.CreateFolder(path, name));
        }

        public Task<string> Delete(string path)
        {
            return Task.FromResult(FileInspector.Delete(path));    
        }

        public async Task Download(FileTransfer fileTransfer)
        {
            IFileTransferContract fileChannel = Program.FileChannelFactory.CreateChannel();

            using (fileTransfer = await fileChannel.Download(fileTransfer))
            {
                using (Stream output = File.Create(fileTransfer.Target))
                {
                    byte[] buffer = new byte[4 * 1024];
                    int length;
                    double progressCheck = 0;

                    while ((length = await fileTransfer.GetFileStream().ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, length);
                        fileTransfer.Progress = Math.Round(((double)output.Position / (double)fileTransfer.FileSize) * (double)100, 0);

                        if (fileTransfer.Progress > progressCheck)
                        {
                            Task.Run(() => fileChannel.ReportFileDownloadStatus(fileTransfer.GetStatus()));
                            progressCheck += fileTransfer.ProgressPercentReport;
                        }
                    }
                }
                fileChannel.ReportFileDownloadCompleted(fileTransfer.GetStatus());
            }
        }

        public Task<List<WindowsItem>> GetDirectoryContents(string path)
        {
            return Task.FromResult(FileInspector.GetDirectoryStructure(path));
        }

        public Task<string> Move(string path, string newPath)
        {
            return Task.FromResult(FileInspector.Move(path, newPath));
        }

        public Task<string> Open(string path)
        {
            return Task.FromResult(FileInspector.Open(path));
        }

        public Task<string> Rename(string path, string newName)
        {
            return Task.FromResult(FileInspector.Rename(path, newName));
        }

        public async Task Upload(FileTransfer fileTransfer)
        {
            try
            {
                IFileTransferContract fileChannel = Program.FileChannelFactory.CreateChannel();

                using (Stream sourceStream = File.OpenRead(fileTransfer.Target))
                {
                    fileTransfer.SetFileStream(sourceStream);
                    await fileChannel.Upload(fileTransfer);
                }
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}

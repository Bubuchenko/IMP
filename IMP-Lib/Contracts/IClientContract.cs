using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Contracts
{
    [ServiceContract]
    public interface IClientContract
    {
        [OperationContract]
        Task<string> CMDCommand(string command);
        [OperationContract]
        Task Upload(FileTransfer fileTransfer);
        [OperationContract]
        Task Download(FileTransfer fileTransfer);

        [OperationContract]
        Task<List<WindowsItem>> GetDirectoryContents(string path);

        [OperationContract]
        Task<string> Open(string path);
        [OperationContract]
        Task<string> Delete(string path);
        [OperationContract]
        Task<string> Rename(string path, string newName);
        [OperationContract]
        Task<string> Move(string path, string newPath);
        [OperationContract]
        Task<string> CreateFile(string path, string name);
        [OperationContract]
        Task<string> CreateFolder(string path, string name);
    }
}

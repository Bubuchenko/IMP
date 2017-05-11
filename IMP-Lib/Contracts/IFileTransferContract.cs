using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFileTransfer" in both code and config file together.
    [ServiceContract]
    public interface IFileTransferContract
    {
        [OperationContract]
        Task Upload(FileTransfer file);
        [OperationContract]
        Task<FileTransfer> Download(FileTransfer fileTransfer);
        [OperationContract]
        void ReportFileDownloadStatus(FileTransferStatus fileTransfer);
        [OperationContract]
        void ReportFileDownloadCompleted(FileTransferStatus fileTransferStatus);
    }
}

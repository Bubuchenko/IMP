using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Web.Models.ViewModels
{
    public class FileBrowserViewModel
    {
        public Client Client { get; set; }
        public string Drive { get; set; }
        public List<WindowsItem> DirectoryStructure { get; set; }
        public string SerializedDirectoryStructure
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(DirectoryStructure);
            }
        }
    }
}
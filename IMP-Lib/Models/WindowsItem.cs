using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    public class WindowsItem
    {
        public string Path { get; set; }
        public WindowsItemType Type { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(Path))
                    return System.IO.Path.GetFileName(Path);
                else
                    return "";
                
            } private set { }
        }

        public string ParentDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(Path))
                    return Directory.GetParent(Path).ToString();
                else
                    return "";
            } private set { }
        }
    }
}

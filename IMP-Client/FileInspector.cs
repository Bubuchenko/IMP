using IMP_Lib.Enums;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Client
{
    public class FileInspector
    {
        public static List<WindowsItem> GetDirectoryStructure(string path)
        {
            path = path + "\\";

            List<WindowsItem> Items = new List<WindowsItem>();

            string[] folders = Directory.GetDirectories(path);
            for (int i = 0; i < folders.Length; i++)
            {
                Items.Add(new WindowsItem
                {
                    Type = WindowsItemType.Folder,
                    LastModified = File.GetLastWriteTimeUtc(folders[i]),
                    Path = folders[i],
                    Size = IsDirectoryEmpty(folders[i]) ? 0 : 1
                });
            }

            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                Items.Add(new WindowsItem
                {
                    Type = WindowsItemType.File,
                    LastModified = File.GetLastWriteTimeUtc(files[i]),
                    Path = files[i],
                    Size = new FileInfo(files[i]).Length
                });
            }

            return Items;
        }

        public static string Delete(string path)
        {
            try
            {
                if(IsDirectory(path))
                    Directory.Delete(path, true);
                else
                    File.Delete(path);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Open(string path)
        {
            try
            {
                Process.Start(path);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message; ;
            }
        }

        public static string Rename(string path, string newName)
        {
            try
            {
                if (IsDirectory(path))
                    File.Move(path, newName);
                else
                    Directory.Move(path, newName);

                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }


        public static bool IsDirectoryEmpty(string path)
        {
            try
            {
                return !Directory.EnumerateFileSystemEntries(path).Any();
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            if ((attr.HasFlag(FileAttributes.Directory)))
                return true;
            else
                return false;
        }
    }
}

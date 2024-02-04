using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hippie.Repositories
{
    public static class FileHelpers
    {
        public static List<FileInfo> GetAllFiles(string path, string filter = null)
        {
            var allFiles = new List<FileInfo>();
            var files = Directory.GetFiles(path, filter);
            foreach (var file in files) { 
                allFiles.Add(new FileInfo(file));
            }

            return allFiles;
        }

        public static string FindGarmentCreatorDir()
        {
            string programX64folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programX86folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string garmentFolderName = "GarmentCreator";
            string garmentExeName = "GarmentCreator.exe";
            string fullX64 = Path.Combine(programX64folder, garmentFolderName, garmentExeName);
            string fullX86 = Path.Combine(programX86folder, garmentFolderName, garmentExeName);

            if (File.Exists(fullX64))
                return fullX64;
            else if (File.Exists(fullX86))
                return fullX86;
            return null;
        }

        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }
    }
}

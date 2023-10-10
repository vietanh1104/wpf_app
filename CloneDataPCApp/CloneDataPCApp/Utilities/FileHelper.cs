using Splat;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CloneDataPCApp.Utilities
{
    public class FileHelper : IEnableLogger
    {
        public static FileHelper Instance = new FileHelper();

        public bool OpenFile(string fileName, string folderName = null)
        {
            try
            {
                var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), folderName ?? string.Empty);
                var file = Path.Combine(filePath, fileName);

                if (!File.Exists(file))
                    return false;

                new Process
                {
                    StartInfo = new ProcessStartInfo(file)
                    {
                        UseShellExecute = true,
                    }
                }.Start();

                return true;
            }
            catch (Exception e)
            {
                this.Log().Error(e);
                return false;
            }
        }

        public bool OpenFileAsAdministrator(string fileName, string folderName = null)
        {
            try
            {
                var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), folderName ?? string.Empty);
                var file = Path.Combine(filePath, fileName);

                if (!File.Exists(file))
                    return false;

                new Process
                {
                    StartInfo = new ProcessStartInfo(file)
                    {
                        UseShellExecute = true,
                        Verb = "runas",
                        WindowStyle = ProcessWindowStyle.Hidden,
                    }
                }.Start();

                return true;
            }
            catch (Exception e)
            {
                this.Log().Error(e);
                return false;
            }
        }
    }
}
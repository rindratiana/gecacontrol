using GECA_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Services
{
    public class FileService
    {
        public static void WriteFile(DoMove doMove,string path)
        {
            try
            {
                string fullDirectoryPath = "";
                if (string.IsNullOrEmpty(path))
                {
                    string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    fullDirectoryPath = Path.Combine(projectDirectory, "");
                }
                else { 
                    fullDirectoryPath = path;
                }
                Directory.CreateDirectory(fullDirectoryPath);
                string fullFilePath = Path.Combine(fullDirectoryPath, "command.log");
                File.AppendAllText(fullFilePath, doMove.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

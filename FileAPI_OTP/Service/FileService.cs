using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI_OTP.Service
{
    public class FileService : IFileService
    {
        private IConfigParam param;
        private string path;
        public FileService(IConfigParam param)
        {
            this.param = param;
            this.path = param.ReadPath();
        }
        public string GetFile(string filename)
        {
            try
            {
                var combinedPath = Path.Combine(path, filename);
                if (filename == null || !File.Exists(combinedPath))
                {
                    throw new Exception("Not found");
                }

                byte[] bytes = System.IO.File.ReadAllBytes(Path.Combine(path, filename));
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetFiles()
        {
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo fInfo in dirInfo.GetFiles())
            {
                files.Add(fInfo.Name);
            }
            return files.ToList();
        }

        public void UploadFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                        using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                }
                else
                {
                    throw new IOException("Is empty");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

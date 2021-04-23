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
            this.path = param.ReadFile();
        }
        public ActionResult<string> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return "";
                var combinedPath = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               path, filename);

                byte[] bytes = System.IO.File.ReadAllBytes(combinedPath);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<string> ListFiles()
        {
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(
                               Directory.GetCurrentDirectory(),
                               path));

            foreach (FileInfo fInfo in dirInfo.GetFiles())
            {
                files.Add(fInfo.Name);
            }
            return files.ToList();
        }

        public void Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0 && file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(
                                   Directory.GetCurrentDirectory(),
                                   "wwwroot", file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

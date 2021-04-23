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
        public ActionResult<string> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return "";
                var path = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", filename);

                byte[] bytes = System.IO.File.ReadAllBytes(path);
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
                               "wwwroot"));
            foreach (FileInfo fInfo in dirInfo.GetFiles())
            {
                files.Add(fInfo.Name);
            }
            return files.ToList();
        }

        public Task<IActionResult> Upload(IFormFile file)
        {
            if (file.Length > 0 && file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return Ok("Sikeres feltöltés! + ");
            }
            return BadRequest();
        }
    }
}

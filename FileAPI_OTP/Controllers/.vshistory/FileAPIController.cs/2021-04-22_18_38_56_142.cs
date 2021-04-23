using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public FileAPIController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("/api/dokumentumok/{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("ÜRES");

                var provider = new FileExtensionContentTypeProvider();
                string contentType = "";
           
           
                var path = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", filename);
                if (!provider.TryGetContentType(path,out contentType))
                {
                    contentType = "application/octet-stream";
                }
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, contentType, Path.GetFileName(path));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/api/dokumentumok/")]
        public IActionResult ListFiles(IFileProvider fileProvider)
        {
            var files = fileProvider.GetDirectoryContents("wwwroot");

            var latestFile =
                      files
                      .OrderByDescending(f => f.LastModified)
                      .FirstOrDefault();

            return Ok(latestFile?.Name);
        }
    }
}

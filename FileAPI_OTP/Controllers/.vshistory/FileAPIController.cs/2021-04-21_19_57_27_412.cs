using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        [HttpGet]
        [Route("/api/dokumentumok/{id}")]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

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
    }
}

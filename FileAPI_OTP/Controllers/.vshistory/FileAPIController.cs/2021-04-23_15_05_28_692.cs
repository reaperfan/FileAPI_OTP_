using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.Buffers.Text;
using Microsoft.Extensions.Configuration;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private string path;
        private Service.IFileService fileService;

        public FileAPIController(IWebHostEnvironment hostingEnvironment, IConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            path = _configuration.GetValue<string>("path");
        }
        [HttpGet]
        [Route("/api/dokumentumok/{filename}")]
        public ActionResult<string> Download(string filename)
        {
            return fileService.Download(filename);
        }

        [HttpGet]
        [Route("/api/dokumentumok/")]
        public IEnumerable<string> ListFiles()
        {
            return fileService.ListFiles();
        }

        [HttpPost("api/dokumentumok/feltoltes")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            return Upload(file);
        }
        

    }
}

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
using FileAPI_OTP.Service;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private string path;
        private IFileService _service;

        public FileAPIController(IWebHostEnvironment hostingEnvironment, IConfiguration config,IFileService service)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _service = service;
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
        public  Task<IActionResult> Upload(IFormFile file)
        {
            return Upload(file);
        }
        

    }
}

﻿using Microsoft.AspNetCore.Mvc;
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
            try
            {
                if (filename == null)
                    return Content("ÜRES");
                var path = Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot", filename);

                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return BadRequest(400);
            }
        }

        [HttpGet]
        [Route("/api/dokumentumok/")]
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

        [HttpPost("api/dokumentumok/feltoltes")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file.Length > 0 && file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(
                               Directory.GetCurrentDirectory(),
                               "wwwroot",file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return Ok("Sikeres feltöltés! + ");
            }
            return BadRequest();
        }
        

    }
}
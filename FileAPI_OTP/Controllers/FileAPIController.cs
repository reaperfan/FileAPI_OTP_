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
using System.Net.Http;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IConfigParam _configuration;
        private string path;
        private IFileService _service;

        public FileAPIController(IWebHostEnvironment hostingEnvironment, IConfigParam config,IFileService service)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _service = service;
        }

        [HttpGet]
        [Route("/api/dokumentumok/{filename}")]
        public IActionResult Download(string filename)
        {
            try
            {
                string result = _service.GetFile(filename);
                if (result.Length > 0)
                {
                    return Ok(result);
                }
                else
                { 
                    return NotFound(); 
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("/api/dokumentumok/")]
        public IActionResult ListFiles()
        {
            try
            {
                List<string> list = _service.GetFiles();
                if (list.Count > 0)
                {
                    return Ok(list);
                }
                else
                {
                    return NoContent();
                }
               
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        [HttpPost("api/dokumentumok/feltoltes")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    _service.UploadFile(file);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }

    }
}

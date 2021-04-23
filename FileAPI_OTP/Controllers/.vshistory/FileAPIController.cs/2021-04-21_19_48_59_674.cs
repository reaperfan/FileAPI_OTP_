﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace FileAPI_OTP.Controllers
{
    public class FileAPIController : Controller
    {
        public IActionResult DownloadFile(string fileName)
        {
            var path = Path.Combine(
               Directory.GetCurrentDirectory(), "wwwroot\\UploadFile\\Journal", fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/x-msdownload", fileName);
        }
    }
}

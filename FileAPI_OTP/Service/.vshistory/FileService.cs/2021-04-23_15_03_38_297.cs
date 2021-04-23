﻿using Microsoft.AspNetCore.Mvc;
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
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI_OTP.Service
{
    interface IFileService
    {
        public ActionResult<string> Download(string filename);
        public IEnumerable<string> ListFiles();
        public void Upload(IFormFile file);
    }
}
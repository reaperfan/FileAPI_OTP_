using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileAPI_OTP.Service
{
    public interface IFileService
    {
        public string GetFile(string filename);
        public List<string> GetFiles();
        public void UploadFile(IFormFile file);
    }
}

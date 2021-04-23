using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileAPI_OTP.Service
{
    public class FileService
    {
        public void Download(string filename)
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
    }
}

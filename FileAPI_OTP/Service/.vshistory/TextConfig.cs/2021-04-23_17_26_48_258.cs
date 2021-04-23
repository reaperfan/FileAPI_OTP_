using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FileAPI_OTP.Service
{
    public class TextConfig : IConfigParam
    {
        IWebHostEnvironment env;
        public TextConfig(IWebHostEnvironment hosting)
        {
            env = hosting;
        }
        public string ReadFile()
        {
            string path = File.ReadAllText(Path.Combine(env.WebRootPath, "fileconfig.txt"));
            string combinedPath = Path.Combine(env.WebRootPath, path);
            if (Directory.Exists(Path.Combine(env.WebRootPath,path)))
            {
                return combinedPath;
            }
            else
            {
                return env.WebRootPath;
            }

            

        }
    }
}

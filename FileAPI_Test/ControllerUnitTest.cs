using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

using FileAPI_OTP.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileAPI_Test
{
    public class ControllerUnitTest
    {
        [Fact]
        public void UploadFile_ReturnBadRequest()
        {

            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);
            var result = controller.Upload(null);

            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void UploadFile_ReturnOk()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);
            var result = controller.Upload(file);

            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public void ListFiles_ReturnsOk()
        {
            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);

            fileservice.Setup(m => m.GetFiles()).Returns(() => new List<string>() {"Txt1.txt", "Alma.txt" });

            controller.ListFiles();

            Assert.IsType<OkObjectResult>(controller.ListFiles());
        }
        [Fact]
        public void ListFiles_ReturnsEmptyList()
        {
            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);
            
            
            Assert.IsType<NoContentResult>(controller.ListFiles());

        }
        [Fact]
        public void Download_ReturnsNotFound()
        {
            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);

            Assert.IsType<NotFoundResult>(controller.Download("yyyyy.yxz"));

        }
        [Fact]
        public void Download_ReturnsOk()
        {
            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);

            fileservice.Setup(m => m.GetFile("yyyyy.yxz")).Returns(() => new string("yyyyy.yxz"));


            Assert.IsType<OkObjectResult>(controller.Download("yyyyy.yxz"));
        }
    }
}

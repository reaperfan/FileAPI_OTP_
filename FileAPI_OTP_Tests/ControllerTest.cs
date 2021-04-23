using Xunit;
using FileAPI_OTP;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Hosting;
using FileAPI_OTP.Service;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileAPI_OTP_Tests
{
    
   
    public class ControllerTest
    {
        [Fact]
        public void UploadFile_ReturnNull()
        {

            var webhost = new Mock<IWebHostEnvironment>();
            var config = new Mock<IConfigParam>();
            var fileservice = new Mock<IFileService>();
            var controller = new FileAPI_OTP.Controllers.FileAPIController(webhost.Object, config.Object, fileservice.Object);
            var result = controller.Upload(null);

            Assert.IsType<BadRequestObjectResult>(result);


        }


    }
}
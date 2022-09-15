using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using static System.Net.WebRequestMethods;

namespace ImageUploadWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public static IHostingEnvironment _environment;
        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment;
        }



        
        [HttpPost]
        public IActionResult Upload( IFormFile files , [FromForm] string type)
        {
            var path = _environment.WebRootPath + @"\uploads\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads\\" + $"{type}")).Root  + $@"\{newFileName}";
                   

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                     
                    }
                    return Ok("The Image Uploaded to " + type + " Class");
                }
            }
            return Ok("error");
        }






    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASAPTASK.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : ControllerBase 
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        public CommonController(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UploadFile(IFormCollection files)
        {
            List<string> paths = new List<string>();
            string PhysicalfilePath = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            string pathURL = "/Uploads/";
            foreach (var formFile in files.Files)
            {   //generate url and physical path
                var fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf("."));
                string FileName = Guid.NewGuid().ToString() + fileExt;
                string filePath = PhysicalfilePath + "/" + FileName;
                pathURL = pathURL + FileName;
                if (formFile.Length > 0)
                {
                    paths.Add(pathURL);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(paths.ElementAt(0));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UploadFile2(IFormCollection files)
        {
            List<string> paths = new List<string>();
            string PhysicalfilePath = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            string pathURL = "/Uploads/";
            foreach (var formFile in files.Files)
            {   //generate url and physical path
                var fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf("."));
                string FileName = Guid.NewGuid().ToString() + fileExt;
                string filePath = PhysicalfilePath + "/" + FileName;
                pathURL = pathURL + FileName;
                if (formFile.Length > 0)
                {
                    paths.Add(pathURL);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new {src = paths.ElementAt(0) });
        }

    }
}

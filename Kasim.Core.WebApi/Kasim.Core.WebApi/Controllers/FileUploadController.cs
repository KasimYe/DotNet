using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kasim.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]//设置前端发送的请求Content-Type为multipart/form-data
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        // GET: api/FileUpload
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileUpload/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FileUpload
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            // full path to file in temp location
            var filePath = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string upload_path = Directory.GetCurrentDirectory() + "/wwwroot";
                    DateTime now = DateTime.Now;
                    if (Directory.Exists(upload_path + "/images/" + now.Year) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(upload_path + "/images/" + now.Year);
                    }
                    if (Directory.Exists(upload_path + "/images/" + now.Year + "/" + now.ToString("MMdd")) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(upload_path + "/images/" + now.Year + "/" + now.ToString("MMdd"));
                    }
                    upload_path = upload_path + "/images/" + now.Year + "/" + now.ToString("MMdd");//新的目录

                    var filename = ContentDispositionHeaderValue
                                    .Parse(formFile.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    string houzhui = filename.Substring(filename.IndexOf("."));
                    var filenamenew = DateTime.Now.ToString("yyyyMMddHHmmssfff") + houzhui;
                    filename = upload_path + "/" + filenamenew;
                    filePath = filename;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation. 
            return Ok(new { count = files.Count, size, filePath });

        }


    }
}

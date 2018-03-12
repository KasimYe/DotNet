using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kasim.Core.WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;

namespace Kasim.Core.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kasim.Core.FileUploadWebApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using Kasim.Core.Common;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Kasim.Core.FileUploadWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "图片上传";
            if (Request != null && !string.IsNullOrEmpty(Request.QueryString.Value))
            {
                var req = Request.QueryString.Value.Remove(0, 1).Trim();
                var json = MySecurity.SDecryptString(req, "yss.yh");
                var _fileMode = JsonConvert.DeserializeObject<Model.FileUploadWebApp.FileModel>(json);
                ViewData["Message"] = _fileMode.Message;
            }            
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

        [HttpPost]
        public async Task<IActionResult> FileSave()
        {
            Model.FileUploadWebApp.FileModel fileMode = null;
            if (!string.IsNullOrEmpty(Request.Form["uid"]))
            {
                var req = Request.Form["uid"].ToString().Remove(0, 1).Trim();
                var json = MySecurity.SDecryptString(req, "yss.yh");
                fileMode = JsonConvert.DeserializeObject<Model.FileUploadWebApp.FileModel>(json);
                fileMode.FileList = new List<Model.FileUploadWebApp.File>();
            }
            else
            {
                return BadRequest("非法参数");
            }
            var now = DateTime.Now;
            var files = Request.Form.Files;            
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;            
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = FileOperate.GetPostfixStr(formFile.FileName).Remove(0, 1); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    var foldPath = "upload/" + now.Year + "/" + now.Month + "/" + now.Day;
                    var filePath = Path.Combine(webRootPath, foldPath, newFileName);
                    FileOperate.FolderCreate(Path.Combine(webRootPath, foldPath));
                    fileMode.FileList.Add(new Model.FileUploadWebApp.File {
                        Name= newFileName,
                        Url= Path.Combine(foldPath, newFileName),
                        Time=now
                    });
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    { 
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size, fileMode });
        }

        [HttpPost]
        public JsonResult Preview()
        {
            return null;
        }
    }
}

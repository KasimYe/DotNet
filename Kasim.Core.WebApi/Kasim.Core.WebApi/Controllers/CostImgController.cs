using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Kasim.Core.Model.WebApi;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.BLL.WebApi;
using Kasim.Core.Common;
using System.Drawing;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Kasim.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CostImg")]
    public class CostImgController : Controller
    {
        private readonly ILogger<CostImgController> _logger;
        private readonly ConnectionStringOptions _conns;
        private readonly ApiOptions _apiOptions;
        private readonly ITaxCostPic28BLL taxCostPic28BLL;
        public CostImgController(ILogger<CostImgController> _logger,IOptions<ConnectionStringOptions> connsOptions, IOptions<ApiOptions> apiOptions)
        {
            _conns = connsOptions.Value;
            _apiOptions = apiOptions.Value;
            taxCostPic28BLL = new TaxCostPic28BLL(_conns);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                TaxCostPic28 taxCostPic28 = new TaxCostPic28() { CostID = id };
                taxCostPic28 = taxCostPic28BLL.GetImgByCostID(taxCostPic28);

                if (taxCostPic28 == null)
                {
                    var tempObj = new { Result = "Image No Found", CostImg = taxCostPic28 };
                    return Json(tempObj);
                }

                if (string.IsNullOrEmpty(taxCostPic28.PicUrl))
                {
                    var tempObj = new { Result = "Image Url Is Null", CostImg = taxCostPic28 };
                    return Json(tempObj);
                }

                string url = _apiOptions.AppImgServer.Tax + taxCostPic28.PicUrl;
                string path = _apiOptions.AppImgSavePath.Path + taxCostPic28.InvoiceID + "_" + taxCostPic28.InvoiceCode + ".jpg";
                ImageDown imgDown = new ImageDown();
                Image originalImage = imgDown.DownUrlPic(url);
                Image thumbnailImage = ImageClass.MakeThumbnail(originalImage, _apiOptions.AppImgThumbnail.Width,
                    _apiOptions.AppImgThumbnail.Height, _apiOptions.AppImgThumbnail.Model);
                if (string.IsNullOrEmpty(taxCostPic28.PicMD5))
                {
                    imgDown.PicSave(thumbnailImage, path);
                    taxCostPic28.PicMD5 = MD5.GetFileMd5(path);
                    imgDown.PicRemove(path);
                    taxCostPic28BLL.SetImgMd5(taxCostPic28);
                }
                taxCostPic28.PicUrl = _apiOptions.AppImgServer.CostImg + taxCostPic28.InvoiceID + "_" + taxCostPic28.InvoiceCode + ".jpg";
                var tempObjSuccess = new { Result = "Success", CostImg = taxCostPic28 };
                return Json(tempObjSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Result = ex.Message };
                return Json(tempObj);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]TaxCostPic28 taxCostPic28)
        {            
            try
            {
                taxCostPic28 = taxCostPic28BLL.GetImgByCostID(taxCostPic28);

                if (taxCostPic28 == null)
                {
                    var tempObj = new { Result = "Image No Found", CostImg = taxCostPic28 };
                    return Json(tempObj);
                }

                if (string.IsNullOrEmpty(taxCostPic28.PicUrl))
                {
                    var tempObj = new { Result = "Image Url Is Null", CostImg = taxCostPic28 };
                    return Json(tempObj);
                }

                string url = _apiOptions.AppImgServer.Tax + taxCostPic28.PicUrl;
                string path = _apiOptions.AppImgSavePath.Path + taxCostPic28.InvoiceID + "_" + taxCostPic28.InvoiceCode + ".jpg";
                ImageDown imgDown = new ImageDown();
                Image originalImage = imgDown.DownUrlPic(url);
                Image thumbnailImage = ImageClass.MakeThumbnail(originalImage, _apiOptions.AppImgThumbnail.Width,
                    _apiOptions.AppImgThumbnail.Height, _apiOptions.AppImgThumbnail.Model);
                if (string.IsNullOrEmpty(taxCostPic28.PicMD5))
                {
                    imgDown.PicSave(thumbnailImage, path);
                    taxCostPic28.PicMD5 = MD5.GetFileMd5(path);
                    imgDown.PicRemove(path);
                    taxCostPic28BLL.SetImgMd5(taxCostPic28);
                }
                taxCostPic28.PicUrl = _apiOptions.AppImgServer.CostImg + taxCostPic28.InvoiceID + "_" + taxCostPic28.InvoiceCode + ".jpg";
                var tempObjSuccess = new { Result = "Success", CostImg = taxCostPic28 };
                return Json(tempObjSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Result = ex.Message };
                return Json(tempObj);
            }
        }

        [HttpGet("{Images}/{id}")]
        public ActionResult Images(string id)
        {
            try
            {
                if (!ImageClass.IsImageFileName(id))
                {
                    string html = "<h1>无效的图片文件名(.jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp).</h1>";
                    return Content(html, "text/html", Encoding.UTF8); // 可以指定文本类型
                }
                
                var url = taxCostPic28BLL.GetImgUrlByFileName(id);
                if (string.IsNullOrEmpty(url))
                {
                    string html = "<h1>图片不存在.</h1>";
                    return Content(html, "text/html", Encoding.UTF8);
                }

                url =_apiOptions.AppImgServer.Tax + url;
                ImageDown imgDown = new ImageDown();
                Image originalImage = imgDown.DownUrlPic(url);
                Image thumbnailImage = ImageClass.MakeThumbnail(originalImage, _apiOptions.AppImgThumbnail.Width,
                    _apiOptions.AppImgThumbnail.Height, _apiOptions.AppImgThumbnail.Model);
                byte[] byteImg = imgDown.ImageToMemoryStream(thumbnailImage).ToArray();
                return (File(byteImg, "image/jpg"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string html = "<h1>"+ ex.Message + ".</h1>";
                return Content(html, "text/html", Encoding.UTF8);
            }
        }
    }
}
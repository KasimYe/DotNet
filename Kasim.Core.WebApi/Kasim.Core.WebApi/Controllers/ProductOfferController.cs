using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.BLL.WebApi;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.Model.WebApi;
using Kasim.Core.Model.WebApi.ProductOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kasim.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductOffer")]
    public class ProductOfferController : Controller
    {
        private readonly ILogger<CostImgController> _logger;
        private readonly ConnectionStringOptions _conns;
        private readonly ApiOptions _apiOptions;
        private readonly IProductOfferBLL productOfferBLL;

        public ProductOfferController(ILogger<CostImgController> _logger, IOptions<ConnectionStringOptions> connsOptions, IOptions<ApiOptions> apiOptions)
        {
            _conns = connsOptions.Value;
            _apiOptions = apiOptions.Value;
            productOfferBLL = new ProductOfferBLL(_conns);
        }

        //// GET: api/ProductOffer
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/ProductOffer/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        /// <summary>
        /// 获取B2B商品促销信息
        /// </summary>
        /// <param name="value">ProductsWebOffer类</param> 
        /// <remarks>
        /// 参数为ERP的商品ID的JSON字符串,格式如下.  
        ///    
        ///     POST /ProductOffer  
        ///     {  
        ///        "ProductID": "12345"
        ///     }  
        ///   
        /// </remarks>
        [HttpPost]
        public ActionResult Post([FromBody]ProductsWebOffer value)
        {
            try
            {
                int productId = productOfferBLL.GetProductIdByPID(value.ProductID);
                List<ProductsWebOffer> list = productOfferBLL.ProductsWebOfferListById(productId);
                if (list == null || list.Count == 0)
                {
                    var tempObj = new { Result = "ProductsWebOffer No Found", ProductsWebOffer = list };
                    return Json(tempObj);
                }
                var tempObjSuccess = new { Result = "Success", ProductsWebOffer = list };
                return Json(tempObjSuccess);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Result = ex.Message };
                return Json(tempObj);
            }
        }

        //// PUT: api/ProductOffer/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.Redis.WebApi.BLL;
using Kasim.Core.Redis.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kasim.Core.Redis.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private ProductBLL bll;
        public ProductController(ILogger<ProductController> logger, IOptions<ConnectionStringOptions> connsOptions)
        {
            _logger = logger;
            bll = new ProductBLL(connsOptions.Value);
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var list = bll.GetProducts();
            return new JsonResult(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = bll.GetProduct(id);
            return new JsonResult(entity);
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

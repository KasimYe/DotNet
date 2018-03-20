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
    [Route("api/Client")]
    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;
        private ClientBLL bll;
        public ClientController(ILogger<ClientController> logger, IOptions<ConnectionStringOptions> connsOptions)
        {
            _logger = logger;
            bll = new ClientBLL(connsOptions.Value);
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var list = bll.GetClients();
            return new JsonResult(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = bll.GetClient(id);
            return new JsonResult(entity);
        }

        // POST: api/Client
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Client/5
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

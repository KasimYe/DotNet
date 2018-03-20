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
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private UserBLL bll;
        public UserController(ILogger<UserController> logger, IOptions<ConnectionStringOptions> connsOptions)
        {
            _logger = logger;
            bll = new UserBLL(connsOptions.Value);
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var list = bll.GetUsersByGroups(new int[] { 5, 6 });
            return new JsonResult(list);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var entity = bll.GetUser(id);
            return new JsonResult(entity);
        }
        
        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/User/5
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

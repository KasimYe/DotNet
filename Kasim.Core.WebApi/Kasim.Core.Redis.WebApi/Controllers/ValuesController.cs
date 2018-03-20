using Kasim.Core.Redis.WebApi.BLL;
using Kasim.Core.Redis.WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kasim.Core.Redis.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private UserBLL bll;
        public ValuesController(ILogger<ValuesController> logger, IOptions<ConnectionStringOptions> connsOptions)
        {
            _logger = logger;
            bll = new UserBLL(connsOptions.Value);
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var list = bll.GetUsersByGroups(new int[] { 5,6});
            return new JsonResult(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = bll.GetUser(id);
            return new JsonResult(entity);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

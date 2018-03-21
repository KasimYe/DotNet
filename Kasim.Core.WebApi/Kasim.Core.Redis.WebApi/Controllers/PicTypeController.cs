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
    [Route("api/PicType")]
    public class PicTypeController : Controller
    {
        private readonly ILogger<PicTypeController> _logger;
        private PicTypeBLL bll;
        public PicTypeController(ILogger<PicTypeController> logger, IOptions<ConnectionStringOptions> connsOptions, IOptions<RedisConfig> redisConf)
        {
            _logger = logger;
            bll = new PicTypeBLL(connsOptions.Value, redisConf.Value);
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var list = bll.GetPicTypes();
            return new JsonResult(list);
        }
    }
}

using Kasim.Core.BLL.WebApi.MIS;
using Kasim.Core.IBLL.WebApi.MIS;
using Kasim.Core.Model.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Kasim.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SaleTaxBill")]
    public class SaleTaxBillController : Controller
    {
        private readonly ILogger<CostImgController> _logger;
        private readonly ConnectionStringOptions _conns;
        private readonly ApiOptions _apiOptions;
        private readonly ISaleTaxBLL bll;
        public SaleTaxBillController(ILogger<CostImgController> logger, IOptions<ConnectionStringOptions> connsOptions, IOptions<ApiOptions> apiOptions)
        {
            _logger = logger;
            _conns = connsOptions.Value;
            _apiOptions = apiOptions.Value;
            bll = new SaleTaxBLL(_conns);
        }
        
        // POST: api/SaleTaxBill
        [HttpPost]
        public ActionResult Post([FromBody]PostData data)
        {
            try
            {                
                var result = bll.GetSaleTaxBills(data.StartDate, data.EndDate,data.ClientId,data.TypeId,data.StatusId,data.FormNumber,data.PId);
                return Json(result);
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
            
            
        }

        public class PostData
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int ClientId { get; set; }
            public int TypeId { get; set; }
            public int StatusId { get; set; }
            public string FormNumber { get; set; }
            public int PId { get; set; }
        }
    }
}

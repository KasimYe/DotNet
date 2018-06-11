using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kasim.Core.BLL.WebApi;
using Kasim.Core.Factory.FlowImport;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.Model.WebApi;
using Kasim.Core.Model.WebApi.FlowImport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kasim.Core.WebApi.Controllers
{
    /// <summary>
    /// BzTT数据传输服务
    /// </summary>
    [Produces("application/json")]
    [Route("api/FlowImport")]
    public class FlowImportController : Controller
    {
        private readonly ILogger<FlowImportController> _logger;
        private readonly ConnectionStringOptions _conns;
        private readonly ApiOptions _apiOptions;
        private readonly IFlowImportBLL bll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="connsOptions"></param>
        /// <param name="apiOptions"></param>
        public FlowImportController(ILogger<FlowImportController> logger, IOptions<ConnectionStringOptions> connsOptions, IOptions<ApiOptions> apiOptions)
        {
            _logger = logger;
            _conns = connsOptions.Value;
            _apiOptions = apiOptions.Value;
            ModelFactory.ConnectionString = _conns.FlowImportConnection;
            bll = new FlowImportBLL();
        }

        #region 商品表
        /// <summary>
        /// 清空商品信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearProducts")]
        public ActionResult ClearProducts()
        {
            try
            {
                int result = bll.TruncateProducts();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="entity">商品对象</param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        public ActionResult AddProduct([FromBody]F_Product entity)
        {
            try
            {
                int result = bll.AddProduct(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

        #region 往来户表
        /// <summary>
        /// 清空往来户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearClients")]
        public ActionResult ClearClients()
        {
            try
            {
                int result = bll.TruncateClients();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增往来户信息
        /// </summary>
        /// <param name="entity">往来户对象</param>
        /// <returns></returns>
        [HttpPost("AddClient")]
        public ActionResult AddClient([FromBody]F_Client entity)
        {
            try
            {
                int result = bll.AddClient(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

        #region 商品价格表
        /// <summary>
        /// 清空商品价格信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearProductPrices")]
        public ActionResult ClearProductPrices()
        {
            try
            {
                int result = bll.TruncateProductPrices();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增商品价格信息
        /// </summary>
        /// <param name="entity">商品价格对象</param>
        /// <returns></returns>
        [HttpPost("AddProductPrice")]
        public ActionResult AddProductPrice([FromBody]F_ProductPrice entity)
        {
            try
            {
                int result = bll.AddProductPrice(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

        #region 客户价格类型表
        /// <summary>
        /// 清空客户价格类型信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearClientPriceTypes")]
        public ActionResult ClearClientPriceTypes()
        {
            try
            {
                int result = bll.TruncateClientPriceTypes();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增客户价格类型信息
        /// </summary>
        /// <param name="entity">客户价格类型对象</param>
        /// <returns></returns>
        [HttpPost("AddClientPriceType")]
        public ActionResult AddClientPriceType([FromBody]F_ClientPriceType entity)
        {
            try
            {
                int result = bll.AddClientPriceType(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

        #region 业务员表
        /// <summary>
        /// 清空业务员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearGusers")]
        public ActionResult ClearGusers()
        {
            try
            {
                int result = bll.TruncateGusers();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增业务员信息
        /// </summary>
        /// <param name="entity">业务员对象</param>
        /// <returns></returns>
        [HttpPost("AddGuser")]
        public ActionResult AddGuser([FromBody]F_Guser entity)
        {
            try
            {
                int result = bll.AddGuser(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion


        #region 库存表
        /// <summary>
        /// 清空库存信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("ClearInvs")]
        public ActionResult ClearInvs()
        {
            try
            {
                int result = bll.TruncateInvs();
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增库存信息
        /// </summary>
        /// <param name="entity">库存对象</param>
        /// <returns></returns>
        [HttpPost("AddInv")]
        public ActionResult AddInv([FromBody]F_Inv entity)
        {
            try
            {
                int result = bll.AddInv(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

        #region 单据表
        /// <summary>
        /// 单据状态置为1
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetFormStatus1")]
        public ActionResult SetFormStatus1()
        {
            try
            {
                int result = bll.SetFormStatus(1);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 单据状态置为2
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetFormStatus2")]
        public ActionResult SetFormStatus2()
        {
            try
            {
                int result = bll.SetFormStatus(2);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增单据头信息
        /// </summary>
        /// <param name="entity">单据头对象</param>
        /// <returns></returns>
        [HttpPost("AddForm")]
        public ActionResult AddForm([FromBody]F_Form entity)
        {
            try
            {
                int result = bll.AddForm(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        /// <summary>
        /// 新增单据明细信息
        /// </summary>
        /// <param name="entity">单据明细对象</param>
        /// <returns></returns>
        [HttpPost("AddFormDetail")]
        public ActionResult AddFormDetail([FromBody]F_FormDetail entity)
        {
            try
            {
                int result = bll.AddFormDetail(entity);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = result };
                return Json(tempObj);
            }
            catch (Exception ex)
            {
                var err = string.Format("Exception:\r\n{0}\r\nJSON:\r\n{1}\r\nMethod:\r\n{2}", ex.Message, JsonConvert.SerializeObject(entity), MethodBase.GetCurrentMethod().Name);
                _logger.LogError(err);
                var tempObj = new { Method = MethodBase.GetCurrentMethod().Name, Result = ex.Message };
                return Json(tempObj);
            }
        }
        #endregion

    }
}

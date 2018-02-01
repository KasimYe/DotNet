using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.BLL.WebApp;
using Kasim.Core.IBLL.WebApp;
using Kasim.Core.Model.WebApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Kasim.Core.BrPbsWebApp.Pages
{
    public class SaleBillModel : PageModel
    {
        [BindProperty]
        public string Vc { get; set; }
        [DisplayName("单据日期")]
        [BindProperty]
        public DateTime SystemDate { get; set; }
        [DisplayName("部门ID")]
        [BindProperty]
        public string StoreID { get; set; }
        [DisplayName("单据ID")]
        [BindProperty]
        public string SaleBillID { get; set; }

        [BindProperty]
        public bool ToJson { get; set; }
        [BindProperty]
        public string PostUrl { get; set; }
        [BindProperty]
        public string ResponseText { get; set; }
        private AppOptions AppOptions;
        public SaleBillModel(IOptions<AppOptions> appOptions)
        {
            AppOptions = appOptions.Value;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Vc")))
            {
                Response.Redirect("/");
            }
            else
            {
                SystemDate = DateTime.Now.AddDays(-7);
                StoreID = "1,15";
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vc = HttpContext.Session.GetString("Vc");
                    var id = SystemDate.ToString("yyyyMMdd") + "|" + StoreID + "|" + (string.IsNullOrEmpty(SaleBillID) ? "" : SaleBillID);

                    PostUrl = string.Format("{0}Action?n={1}{2}&m=GetSaleBillsBySystemDate&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), AppOptions.ServiceName, id); ;
                    var url = string.Format("{0}Action?n={1}{2}&z=1&m=GetSaleBillsBySystemDate&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : "&j=0"), AppOptions.ServiceName, id);

                    IPbsClientBLL bll = new PbsClientBLL();
                    var result = bll.GetResponse(url);
                    ResponseText = result;
                }
                catch (Exception ex)
                {
                    ResponseText = ex.Message;
                }
            }
        }
    }
}
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
    public class OrderBillModel : PageModel
    {
        [BindProperty]
        public string Vc { get; set; }
        [DisplayName("Post Data")]
        [BindProperty]
        public string PostData { get; set; }

        [BindProperty]
        public bool ToJson { get; set; }
        [BindProperty]
        public string PostUrl { get; set; }
        [BindProperty]
        public string ResponseText { get; set; }
        private AppOptions AppOptions;
        public OrderBillModel(IOptions<AppOptions> appOptions)
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
                PostUrl = string.Format("{0}Action?n={1}{2}&m=SubOrders&p={3}", AppOptions.ServiceUrl, HttpContext.Session.GetString("Vc"), (ToJson ? "&j=1" : ""), AppOptions.ServiceName);
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vc = HttpContext.Session.GetString("Vc");
                    PostUrl = string.Format("{0}Action?n={1}{2}&m=SubOrders&p={3}", AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), AppOptions.ServiceName);
                    var url = string.Format("{0}Action?n={1}{2}&m=SubOrders&p={3}", AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), "TestBB");

                    IPbsClientBLL bll = new PbsClientBLL();
                    var result = bll.PostWebClient(url, PostData);
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class ClientModel : PageModel
    {
        [BindProperty]
        public string Vc { get; set; }
        [BindProperty]
        public string ClientName { get; set; }
        [BindProperty]
        public bool ToJson { get; set; }
        [BindProperty]
        public string PostUrl { get; set; }
        [BindProperty]
        public string ResponseText { get; set; }
        private AppOptions AppOptions;

        public ClientModel(IOptions<AppOptions> appOptions)
        {
            AppOptions = appOptions.Value;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Vc")))
            {
                Response.Redirect("/");
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vc = HttpContext.Session.GetString("Vc");
                    PostUrl = string.Format("{0}Action?n={1}{2}&m=GetClientInfoByClientName&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), AppOptions.ServiceName, ClientName); ;
                    var url = string.Format("{0}Action?n={1}{2}&z=1&m=GetClientInfoByClientName&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : "&j=0"), AppOptions.ServiceName, ClientName);

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
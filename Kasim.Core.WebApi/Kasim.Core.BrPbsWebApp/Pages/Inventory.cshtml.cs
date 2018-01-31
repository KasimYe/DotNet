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
    public class InventoryModel : PageModel
    {
        [BindProperty]
        public string Vc { get; set; }
        [BindProperty]
        public bool IsHYProduct { get; set; }
        [DisplayName("商品ID")]
        [BindProperty]
        public string PName { get; set; }

        [BindProperty]
        public bool ToJson { get; set; }
        [BindProperty]
        public string PostUrl { get; set; }
        [BindProperty]
        public string ResponseText { get; set; }
        private AppOptions AppOptions;
        public InventoryModel(IOptions<AppOptions> appOptions)
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

                    PostUrl = string.Format("{0}Action?n={1}{2}&m=GetInventoryByPID&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), (IsHYProduct? "B2BServer" : AppOptions.ServiceName), PName); ;
                    var url = string.Format("{0}Action?n={1}{2}&z=1&m=GetInventoryByPID&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : "&j=0"), AppOptions.ServiceName, PName);

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
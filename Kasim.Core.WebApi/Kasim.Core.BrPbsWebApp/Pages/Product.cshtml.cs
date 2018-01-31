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
    public class ProductModel : PageModel
    {
        [BindProperty]
        public string Vc { get; set; }
        [BindProperty]
        public bool IsHYProduct { get; set; }
        [DisplayName("更新时间")]
        [BindProperty]
        public DateTime EditDate { get; set; }
        [BindProperty]
        public bool IsInv { get; set; }
        [DisplayName("商品名称")]
        [BindProperty]
        public string PName { get; set; }
        [BindProperty]
        public bool IsZ { get; set; }
        [BindProperty]
        public bool IsStop { get; set; }

        [BindProperty]
        public bool ToJson { get; set; }
        [BindProperty]
        public string PostUrl { get; set; }
        [BindProperty]
        public string ResponseText { get; set; }
        private AppOptions AppOptions;
        public ProductModel(IOptions<AppOptions> appOptions)
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
                EditDate = DateTime.Now.AddYears(-1);
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Vc = HttpContext.Session.GetString("Vc");
                    var id = (IsHYProduct ? "1|" : "0|") + EditDate.ToString("yyyyMMdd") + (IsInv ? "|1|" : "|0|")
                        + (string.IsNullOrEmpty(PName) ? "" : PName) +(IsZ?"|z":"|f")+(IsStop? "|stop" : "|sure");
                    
                    PostUrl = string.Format("{0}Action?n={1}{2}&m=GetProductInfoByPName&p={3}&id={4}",
                        AppOptions.ServiceUrl, Vc, (ToJson ? "&j=1" : ""), AppOptions.ServiceName, id); ;
                    var url = string.Format("{0}Action?n={1}{2}&z=1&m=GetProductInfoByPName&p={3}&id={4}",
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
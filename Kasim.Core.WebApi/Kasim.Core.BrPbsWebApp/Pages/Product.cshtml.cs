using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.Model.WebApp;
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
            EditDate = DateTime.Now;
        }
    }
}
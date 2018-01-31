using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kasim.Core.BrPbsWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "授权码不能为空")]
        [DisplayName("授权码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "授权码不能大于{2} 且要小于{1}")]
        [BindProperty]
        public string Vc { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (!string.IsNullOrEmpty(Vc))
            {
                HttpContext.Session.SetString("Vc", Vc);
                Response.Redirect("Client");
            }            
        }
    }
}

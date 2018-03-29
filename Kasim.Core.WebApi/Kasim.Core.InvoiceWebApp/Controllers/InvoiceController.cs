using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Hangfire.Dashboard;
using Kasim.Core.BLL.InvoiceWebApp;
using Kasim.Core.Common;
using Kasim.Core.IBLL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kasim.Core.InvoiceWebApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ConnectionStringOptions _conns;
        private readonly ILogger<InvoiceController> _logger;
        public InvoiceController(ILogger<InvoiceController> logger, IOptions<ConnectionStringOptions> connsOptions)
        {
            _logger = logger;
            _conns = connsOptions.Value;
        }
        // GET: Invoice
        [Authorize]
        public ActionResult Index()
        {
            IInvoiceBLL bll = new InvoiceBLL(_conns); ;
            DateTime startDate, endDate;
            startDate = DateTime.Parse("2018-03-22");
            endDate = DateTime.Parse("2018-03-22");
            List<Invoice> list = bll.GetInvoices(startDate, endDate);
            var json = JsonConvert.SerializeObject(list);
            return View(list);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                IInvoiceBLL bll = new InvoiceBLL(_conns);

                string sjm = bll.GetFiveOneFp(id, out Invoice invoice);
                if (sjm == "null")
                {
                    return Content("未找到电子发票，请确定此为电子发票");
                }
                else if (sjm == "TimeOut")
                {
                    return Content("查询超时，请稍后重新尝试");
                }
                else if (sjm.IndexOf(".pdf") > 0)
                {
                    var datePath = string.Format("{0}/{1}/{2}/", invoice.InvoiceDate.Year, invoice.InvoiceDate.Month, invoice.InvoiceDate.ToString("yyyy-MM-dd"));
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdf/", datePath);
                    var filename = sjm;
                    if (System.IO.File.Exists(Path.Combine(path, filename)))
                    {
                        FileStream fs = new FileStream(path + filename, FileMode.Open, FileAccess.Read);
                        byte[] buffer = new byte[Convert.ToInt32(fs.Length)];
                        fs.Read(buffer, 0, Convert.ToInt32(fs.Length));
                        return File(buffer, "application/pdf");
                    }
                    else
                    {
                        return File(DownPdf(bll, invoice, sjm), "application/pdf");
                    }
                }
                else
                {
                    return File(DownPdf(bll, invoice, sjm), "application/pdf");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Invoice Exception:{0}", ex.Message);
                return Content(ex.Message);
            }
        }

        private byte[] DownPdf(IInvoiceBLL bll, Invoice invoice, string sjm)
        {
            var jsonData = (JObject)JsonConvert.DeserializeObject(sjm);
            var xzm = jsonData["xzm"].Value<string>();
            var url = "http://pdf.51fapiao-nb.com/" + xzm;
            var datePath = string.Format("{0}/{1}/{2}/", invoice.InvoiceDate.Year, invoice.InvoiceDate.Month, invoice.InvoiceDate.ToString("yyyy-MM-dd"));
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdf/", datePath);
            var filename = WebDownload.DownLoad2(url, path);
            bll.SetInvoice(invoice.FormNumber, filename);
            FileStream fs = new FileStream(path + filename, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[Convert.ToInt32(fs.Length)];
            fs.Read(buffer, 0, Convert.ToInt32(fs.Length));
            return buffer;
        }

        // GET: Invoice/Edit/5
        public async Task<ActionResult> AdminAsync(string id)
        {           
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            principal.AddIdentity(new ClaimsIdentity { Label = id });
            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", principal);
            return Redirect("/hangfire");
        }


    }
}
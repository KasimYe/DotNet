using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.BLL.InvoiceWebApp;
using Kasim.Core.Common;
using Kasim.Core.IBLL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
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
            IInvoiceBLL bll = new InvoiceBLL(_conns);
            string sjm = bll.GetFiveOneFp(id);
            if (sjm == "TimeOut")
            {
                return Content("查询超时，请稍后重新尝试");
            }
            else
            {
                var jsonData = (JObject)JsonConvert.DeserializeObject(sjm);
                var xzm = jsonData["xzm"].Value<string>();
                var url = "http://pdf.51fapiao-nb.com/" + xzm;
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdf/");
                var filename = WebDownload.DownLoad2(url, path);
                FileStream fs = new FileStream(path+filename, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[Convert.ToInt32(fs.Length)];
                fs.Read(buffer, 0, Convert.ToInt32(fs.Length));
                return File(buffer, "application/pdf");
            }
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
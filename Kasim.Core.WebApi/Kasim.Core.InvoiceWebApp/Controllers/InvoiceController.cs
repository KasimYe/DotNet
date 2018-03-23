using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.BLL.InvoiceWebApp;
using Kasim.Core.IBLL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            startDate = DateTime.Now.AddDays(-1);
            endDate = DateTime.Now;
            List<Invoice> list = bll.GetInvoices(startDate, endDate);
            return View(list);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(string id)
        {
            IInvoiceBLL bll = new InvoiceBLL(_conns);
            string sjm = bll.GetFiveOneFp(id);
            var url = "http://www.51fapiao-nb.com/tycx-jg.html?user_type=&appid=&sjm="+sjm;
            return Redirect(url);
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
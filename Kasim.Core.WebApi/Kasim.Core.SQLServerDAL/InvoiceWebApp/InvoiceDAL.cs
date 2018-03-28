/*
                 ___====-_  _-====___
           _--^^^#####//      \\#####^^^--_
        _-^##########// (    ) \\##########^-_
       -############//  |\^^/|  \\############-
     _/############//   (@::@)   \\############\_
    /#############((     \\//     ))#############\
   -###############\\    (oo)    //###############-
  -#################\\  / VV \  //#################-
 -###################\\/      \//###################-
_#/|##########/\######(   /\   )######/\##########|\#_
|/ |#/\#/\#/\/  \#/\##\  |  |  /##/\#/  \/\#/\#/\#| \|
`  |/  V  V  `   V  \#\| |  | |/#/  V   '  V  V  \|  '
   `   `  `      `   / | |  | | \   '      '  '   '
                    (  | |  | |  )
                   __\ | |  | | /__
                  (vvv(VVV)(VVV)vvv)                  

* Filename: InvoiceDAL
* Namespace: Kasim.Core.SQLServerDAL.InvoiceWebApp
* Classname: InvoiceDAL
* Created: 2018-03-22 21:08:28
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.IDAL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.InvoiceWebApp
{
    public class InvoiceDAL : IInvoiceDAL
    {
        private ConnectionStringOptions _connsOptions;

        public InvoiceDAL(ConnectionStringOptions connsOptions)
        {
            _connsOptions = connsOptions;
        }

        public Invoice GetEntity(string id)
        {
            using (var Conn = new SqlConnection(_connsOptions.DevConnection))
            {
                string query = "SELECT FormNumber,SystemDate,FPHM AS InvoiceCode,FPDM AS InvoiceId,KPRQ AS InvoiceDate,TaxPaidSum AS InvoiceSum,PdfFileName FROM dbo.SaleTaxBill WHERE FormNumber=@id AND Status=11";
                var result = Conn.Query<Invoice>(query, new { id }).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public Invoice GetLastEntity()
        {
            using (var Conn = new SqlConnection(_connsOptions.DevConnection))
            {
                string query = "SELECT TOP 1 FormNumber,SystemDate,FPHM AS InvoiceCode,FPDM AS InvoiceId,KPRQ AS InvoiceDate,TaxPaidSum AS InvoiceSum,PdfFileName FROM dbo.SaleTaxBill WHERE Status=11 AND PdfFileName IS NULL ORDER BY BillID DESC";
                var result = Conn.Query<Invoice>(query).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<Invoice> GetList(DateTime startDate, DateTime endDate)
        {            
            using (var Conn = new SqlConnection(_connsOptions.DevConnection))
            {
                string query = "SELECT TOP 10 FormNumber,SystemDate,FPHM AS InvoiceCode,FPDM AS InvoiceId,KPRQ AS InvoiceDate,TaxPaidSum AS InvoiceSum,PdfFileName FROM dbo.SaleTaxBill WHERE SystemDate>=@startDate AND SystemDate<=@endDate AND Status=11 ORDER BY FormNumber";
                var result = Conn.Query<Invoice>(query,new { startDate, endDate }).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int SetEntity(string id, string filename)
        {
            using (var Conn = new SqlConnection(_connsOptions.DevConnection))
            {
                string query = "UPDATE dbo.SaleTaxBill SET PdfFileName=@filename WHERE FormNumber=@id";
                var result = Conn.Execute(query, new { filename, id });
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

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

* Filename: SaleTaxDAL
* Namespace: Kasim.Core.SQLServerDAL.WebApi
* Classname: SaleTaxDAL
* Created: 2018-03-26 13:13:18
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.IDAL.WebApi.MIS;
using Kasim.Core.Model.WebApi;
using Kasim.Core.Model.WebApi.MIS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.WebApi.MIS
{
    public class SaleTaxDAL : ISaleTaxDAL<SaleTaxBill>
    {
        private ConnectionStringOptions _conns;

        public SaleTaxDAL(ConnectionStringOptions conns)
        {
            _conns = conns;
        }

        public List<SaleTaxBill> GetList(DateTime startDate, DateTime endDate, int clientId, int taxTypeId = -1, int taxStatusId = -1, string formNumber = "", int pId = -1)
        {
            using (var Conn = new SqlConnection(_conns.DevConnection))
            {
                var query = new StringBuilder("SELECT BillID,FormNumber,ClientID,BankName,AccountNumber,TaxNumber,Address,Telephone,SystemDate,CASE FormTypeID WHEN 1 THEN '普通发票' ELSE '专用发票' END AS FormTypeName,CASE Status WHEN 1 THEN '纸质发票' ELSE '电子发票' END AS StatusTypeName,"
                    + "FPHM,FPDM,KPRQ,TaxPaidSum,Notes FROM dbo.SaleTaxBill WHERE SystemDate>=@startDate AND SystemDate<=@endDate AND Status IN (1,11) AND ClientID=@clientId");
                var param = new DynamicParameters();
                param.Add("@startDate", startDate);
                param.Add("@endDate", endDate);
                param.Add("@clientId", clientId);
                if (taxTypeId != -1)
                {
                    query.Append(" AND FormTypeID=@taxTypeId");
                    param.Add("@taxTypeId", taxTypeId);
                }
                if (taxStatusId != -1)
                {
                    query.Append(" AND Status=@taxStatusId");
                    param.Add("@taxStatusId", taxStatusId);
                }
                if (formNumber != "")
                {
                    query.AppendFormat(" AND Notes LIKE '%{0}%'", formNumber);
                }
                if (pId != -1)
                {
                    query.Append(" AND EXISTS (SELECT p.ID FROM dbo.SaleTaxBillDetail d,dbo.SaleTaxBillDetailProducts p WHERE d.DetailID=p.DetailID AND d.BillID=dbo.SaleTaxBill.BillID AND p.PID=@pId)");
                    param.Add("@pId", pId);
                }
                query.Append(" Order By BillID");
                var result=Conn.Query<SaleTaxBill>(query.ToString(), param).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

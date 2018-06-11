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

* Filename: TaxBillDAL
* Namespace: Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
* Classname: TaxBillDAL
* Created: 2018-05-04 15:02:31
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Factory.Weixin;
using Kasim.Core.IDAL.WeiXin.MP.WebApp;
using Kasim.Core.Model.Weixin.MP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
{
    public class TaxBillDAL : ITaxBillDAL
    {
        public List<TaxBill> GetList()
        {
            using (IDbConnection connection = ModelFactory.OpenConnection(ModelFactory.ConnectionStringOptions.TaxConnection))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT DJH AS FormNumber,KHMC AS ClientName,FPHM AS TaxFphm,FPDM AS TaxFpdm,FPKPRQ AS TaxFprq,BZ AS Note,CONVERT(DECIMAL(18,2),SUM(HSJE-ZK)) AS TaxTotal,CONVERT(DECIMAL(18,2),SUM((HSJE-ZK)/(1+SLV/100))) AS NoTaxTotal,CONVERT(DECIMAL(18,2),SUM(HSJE-ZK)-SUM((HSJE-ZK)/(1+SLV/100))) AS PuTotal,DJZT");
                builder.Append(string.Format(" FROM dbo.JSKPXX WHERE DJRQ='{0}' AND DJZT IN (1,11)", DateTime.Now.AddDays(-1).ToShortDateString()));
                builder.Append(" GROUP BY DJH,KHMC,FPHM,FPDM,FPKPRQ,BZ,DJZT ORDER BY DJH");
                var result = connection.Query<TaxBill>(builder.ToString()).ToList();
                connection.Close();
                connection.Dispose();
                return result;
            }
        }
    }
}

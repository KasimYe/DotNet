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

* Filename: QhProductDAL
* Namespace: Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
* Classname: QhProductDAL
* Created: 2018-05-04 10:54:43
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
    public class QhProductDAL : IQhProductDAL
    {
        public List<QhProduct> GetList()
        {
            using (IDbConnection connection = ModelFactory.OpenConnection(ModelFactory.ConnectionStringOptions.DevConnection))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT q.QHID AS Id,q.SystemDate,c.ClientName,PName,Model,FromPlace,QHQ AS OrderQty,InDate,(SELECT UserName FROM dbo.Users WHERE UserID=cpt.SalesID) AS SalesName,InQty ");
                builder.Append("FROM dbo.QHProducts q,dbo.Products p,dbo.Clients c,dbo.ClientPriceTypes cpt ");
                builder.Append("WHERE q.PID=p.PID AND q.ClientID=c.ClientID AND c.ClientID=cpt.ClientID AND q.StoreID=cpt.StoreID ");
                builder.Append(string.Format("AND cpt.StoreID=1 AND InDate >= '{0}'", DateTime.Now.ToShortDateString()));
                var result = connection.Query<QhProduct>(builder.ToString()).ToList();
                connection.Close();
                connection.Dispose();
                return result;
            }
        }
    }
}

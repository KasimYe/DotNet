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

* Filename: ProductDAL
* Namespace: Kasim.Core.Redis.WebApi.DAL.Sql
* Classname: ProductDAL
* Created: 2018-03-20 22:53:53
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.Redis.WebApi.DAL.Sql
{
    public class ProductDAL : BaseDAL<Product>
    {
        public override List<Product> GetList()
        {
            using (IDbConnection Conn = new SqlConnection(Conf.ConStrOps.DevConnection))
            {
                string query = "SELECT PID,PCode,PName,PYName,UnitType,Alias,Model,FromPlace,ShortFromPlace,PuRate,BaseUnit,UnitRate,MidUnitRate,MinUnitRate,LSPrice,PFPrice,BrTaxCode,Status FROM dbo.Products";
                var result = Conn.Query<Product>(query).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

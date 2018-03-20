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

* Filename: ClientDAL
* Namespace: Kasim.Core.Redis.WebApi.DAL.Sql
* Classname: ClientDAL
* Created: 2018-03-20 22:45:18
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Redis.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.Redis.WebApi.DAL.Sql
{
    public class ClientDAL : BaseDAL<Client>
    {
        public override List<Client> GetList()
        {
            using (IDbConnection Conn = new SqlConnection(Factory.Conf.ConStrOps.DevConnection))
            {
                string query = "SELECT ClientID,ClientCode,ClientName,PYName,Address,WarehouseAddress,TransLine FROM dbo.Clients";
                var result = Conn.Query<Client>(query).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

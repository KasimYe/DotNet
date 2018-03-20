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

* Filename: UserDAL
* Namespace: Kasim.Core.Redis.WebApi.DAL.Sql
* Classname: UserDAL
* Created: 2018-03-20 22:10:03
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
    public class UserDAL:BaseDAL<User>
    {
        public override List<User> GetList()
        {
            using (IDbConnection Conn = new SqlConnection(Conf.ConStrOps.DevConnection))
            {
                string query = "SELECT UserId,UserName,UserGroupId FROM Users";
                var result = Conn.Query<User>(query).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }        
    }
}

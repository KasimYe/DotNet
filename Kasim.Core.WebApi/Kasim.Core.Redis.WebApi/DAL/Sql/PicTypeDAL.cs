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

* Filename: PicTypeDAL
* Namespace: Kasim.Core.Redis.WebApi.DAL.Sql
* Classname: PicTypeDAL
* Created: 2018-03-21 19:39:47
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
    public class PicTypeDAL : BaseDAL<PicType>
    {
        

        public override List<PicType> GetList()
        {
            using (IDbConnection Conn = new SqlConnection(Factory.Conf.ConStrOps.DevConnection))
            {
                string query = "SELECT PicTypeID,PicTypeName,bProduct FROM dbo.PicTypes";
                var result = Conn.Query<PicType>(query).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }            
        }
    }
}

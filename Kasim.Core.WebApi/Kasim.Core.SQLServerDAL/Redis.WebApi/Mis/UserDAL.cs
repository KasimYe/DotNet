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
* Namespace: Kasim.Core.SQLServerDAL.Redis.WebApi.Mis
* Classname: UserDAL
* Created: 2018-03-20 19:33:48
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.Redis.WebApi.Mis;
using Kasim.Core.Model.Redis.WebApi;
using Kasim.Core.Model.Redis.WebApi.Mis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.Redis.WebApi.Mis
{
    public class UserDAL : IUserDAL<User>
    {
        public int Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetEntity(object id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetList()
        {
            ConnectionFactory.ConnectionString = Conf.ConStrOps.DevConnection;
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT UserId,UserName,UserGroupId FROM Users";
                var result = Conn.Query<User>(query).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<User> GetListByGroupId(int userGroupId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetListByGroupIds(int[] userGroupIds)
        {
            throw new NotImplementedException();
        }

        public int Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public int Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

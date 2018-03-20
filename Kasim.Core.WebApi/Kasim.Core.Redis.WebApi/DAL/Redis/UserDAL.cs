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
* Namespace: Kasim.Core.Redis.WebApi.DAL.Redis
* Classname: UserDAL
* Created: 2018-03-20 22:10:10
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.Common.RedisHelp;
using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;

namespace Kasim.Core.Redis.WebApi.DAL.Redis
{
    public class UserDAL:BaseDAL<User>
    {
        private RedisHelper redis;
        Sql.UserDAL dal;
        public UserDAL()
        {
            redis = new RedisHelper(0, Conf.ConStrOps.DefaultConnection);
            dal = new Sql.UserDAL();
        }

        public override List<User> GetList()
        {
            if (redis.KeyExists("Users"))
            {
                return redis.ListRange<User>("Users");
            }
            else
            {
                var list = dal.GetList();
                list.ForEach(x => redis.ListRightPush("Users", x));
                redis.KeyExpire("Users", Conf.ExpiryDate);
                return list;
            }
        }

        internal User GetEntity(int userId)
        {
            throw new NotImplementedException();
        }     

        internal List<User> GetListByGroupId(int userGroupId)
        {
            return GetList().Where(x => x.UserGroupId == userGroupId).ToList();
        }

        internal List<User> GetListByGroupIds(int[] userGroupIds)
        {
            var list = from u in GetList() where userGroupIds.Contains(u.UserGroupId) select u;
            return list.ToList();
        }
    }
}

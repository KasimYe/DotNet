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
* Namespace: Kasim.Core.RedisDAL.Redis.WebApi.Mis
* Classname: UserDAL
* Created: 2018-03-20 19:47:46
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Common.RedisHelp;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.Redis.WebApi.Mis;
using Kasim.Core.Model.Redis.WebApi;
using Kasim.Core.Model.Redis.WebApi.Mis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kasim.Core.RedisDAL.Redis.WebApi.Mis
{
    public class UserDAL<T> : IUserDAL<User>
    {
        private RedisHelper redis;
        IUserDAL<User> dal;
        public UserDAL()
        {
            redis = new RedisHelper(1,Conf.ConStrOps.DefaultConnection);
            dal = DALFactory<IUserDAL<User>>.CreateDAL("Kasim.Core.SQLServerDAL", "Redis.WebApi.Mis.UserDAL");
        }
        public int Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetEntity(object id)
        {
           return GetList().Where(x => x.UserId == Convert.ToInt32(id)).SingleOrDefault();
        }

        public List<User> GetList()
        {
            if (redis.KeyExists("Users"))
            {
                return redis.ListRange<User>("Users");
            }
            else
            {
                var list = dal.GetList();
                list.ForEach(x => redis.ListRightPush("Users", x));
                return list;
            }
        }

        public List<User> GetListByGroupId(int userGroupId)
        {
            return GetList().Where(x => x.UserGroupId == userGroupId).ToList();
        }

        public List<User> GetListByGroupIds(int[] userGroupIds)
        {
            var list= from u in GetList() where userGroupIds.Contains(u.UserGroupId) select u;
            return list.ToList();
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

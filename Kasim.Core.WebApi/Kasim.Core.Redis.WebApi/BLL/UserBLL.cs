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

* Filename: UserBLL
* Namespace: Kasim.Core.Redis.WebApi.BLL
* Classname: UserBLL
* Created: 2018-03-20 22:09:14
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.Redis.WebApi.BLL
{
    public class UserBLL
    {
        DAL.Redis.UserDAL dal;
        public UserBLL(ConnectionStringOptions cso, RedisConfig rc)
        {
            Conf.ConStrOps = cso;
            Conf.ExpiryDate = new TimeSpan(rc.TsDays, rc.TsHours, rc.TsMinutes, rc.TsSeconds);
            dal = new DAL.Redis.UserDAL();
        }

        public User GetUser(int userId)
        {
            return dal.GetEntity(userId);
        }

        public List<User> GetUsersByGroup(int userGroupId)
        {
            return dal.GetListByGroupId(userGroupId);
        }

        public List<User> GetUsersByGroups(int[] userGroupIds)
        {
            return dal.GetListByGroupIds(userGroupIds);
        }

        internal List<User> GetUsers()
        {
            return dal.GetList();
        }
    }
}

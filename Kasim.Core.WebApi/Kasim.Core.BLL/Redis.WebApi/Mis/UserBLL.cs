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
* Namespace: Kasim.Core.BLL.Redis.WebApi.Mis
* Classname: UserBLL
* Created: 2018-03-20 19:39:00
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Factory;
using Kasim.Core.IBLL.Redis.WebApi.Mis;
using Kasim.Core.IDAL.Redis.WebApi.Mis;
using Kasim.Core.Model.Redis.WebApi;
using Kasim.Core.Model.Redis.WebApi.Mis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.Redis.WebApi.Mis
{
    public class UserBLL: IUserBLL
    {
        IUserDAL<User> dal;//DALFactory<IUserDAL<User>>.CreateDAL("Kasim.Core.RedisDAL", "Redis.WebApi.Mis.UserDAL");   
        public UserBLL(ConnectionStringOptions value)
        {
            Conf.ConStrOps = value;
            dal = new RedisDAL.Redis.WebApi.Mis.UserDAL<User>();
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
    }
}

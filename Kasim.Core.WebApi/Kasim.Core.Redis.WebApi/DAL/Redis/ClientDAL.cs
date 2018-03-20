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
* Namespace: Kasim.Core.Redis.WebApi.DAL.Redis
* Classname: ClientDAL
* Created: 2018-03-20 22:45:07
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Common.RedisHelp;
using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.Redis.WebApi.DAL.Redis
{
    public class ClientDAL : BaseDAL<Client>
    {
        private RedisHelper redis;
        Sql.ClientDAL dal;
        public ClientDAL()
        {
            redis = new RedisHelper(1, Conf.ConStrOps.DefaultConnection);
            dal = new Sql.ClientDAL();
        }

        public override List<Client> GetList()
        {
            if (redis.KeyExists("Clients"))
            {
                return redis.ListRange<Client>("Clients");
            }
            else
            {
                var list = dal.GetList();
                list.ForEach(x => redis.ListRightPush("Clients", x));
                redis.KeyExpire("Clients", Conf.ExpiryDate);
                return list;
            }
        }
    }
}

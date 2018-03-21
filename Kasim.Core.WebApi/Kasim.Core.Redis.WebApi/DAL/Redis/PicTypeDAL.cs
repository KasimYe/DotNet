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
* Namespace: Kasim.Core.Redis.WebApi.DAL.Redis
* Classname: PicTypeDAL
* Created: 2018-03-21 19:40:32
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Common.RedisHelp;
using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.Redis.WebApi.DAL.Redis
{
    public class PicTypeDAL : BaseDAL<PicType>
    {
        private RedisHelper redis;
        Sql.PicTypeDAL dal;
        public PicTypeDAL()
        {
            redis = new RedisHelper(1, Conf.ConStrOps.DefaultConnection);
            dal = new Sql.PicTypeDAL();
        }

        public override List<PicType> GetList()
        {
            if (redis.KeyExists("PicTypes"))
            {
                return redis.ListRange<PicType>("PicTypes");
            }
            else
            {
                var list = dal.GetList();
                list.ForEach(x => redis.ListRightPush("PicTypes", x));
                redis.KeyExpire("PicTypes", Conf.ExpiryDate);
                return list;
            }
        }
    }
}

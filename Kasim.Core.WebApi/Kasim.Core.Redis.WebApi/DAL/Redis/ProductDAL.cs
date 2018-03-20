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

* Filename: ProductDAL
* Namespace: Kasim.Core.Redis.WebApi.DAL.Redis
* Classname: ProductDAL
* Created: 2018-03-20 22:53:46
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
    public class ProductDAL : BaseDAL<Product>
    {
        private RedisHelper redis;
        Sql.ProductDAL dal;
        public ProductDAL()
        {
            redis = new RedisHelper(0, Conf.ConStrOps.DefaultConnection);
            dal = new Sql.ProductDAL();
        }

        public override List<Product> GetList()
        {
            if (redis.KeyExists("Products"))
            {
                return redis.ListRange<Product>("Products");
            }
            else
            {
                var list = dal.GetList();
                list.ForEach(x => redis.ListRightPush("Products", x));
                redis.KeyExpire("Products", Conf.ExpiryDate);
                return list;
            }
        }
    }
}

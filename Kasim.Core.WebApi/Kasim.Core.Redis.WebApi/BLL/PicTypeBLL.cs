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

* Filename: PicTypeBLL
* Namespace: Kasim.Core.Redis.WebApi.BLL
* Classname: PicTypeBLL
* Created: 2018-03-21 19:39:22
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
    public class PicTypeBLL
    {
        DAL.Redis.PicTypeDAL dal;
        public PicTypeBLL(ConnectionStringOptions cso, RedisConfig rc)
        {
            Conf.ConStrOps = cso;
            Conf.ExpiryDate = new TimeSpan(1, 1, 1, 1);
            dal = new DAL.Redis.PicTypeDAL();
        }

        public List<PicType> GetPicTypes()
        {
            return dal.GetList();
        }
    }
}

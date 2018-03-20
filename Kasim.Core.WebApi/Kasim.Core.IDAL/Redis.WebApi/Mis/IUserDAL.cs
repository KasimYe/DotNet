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

* Filename: IUserDAL
* Namespace: Kasim.Core.IDAL.Redis.WebApi.Mis
* Classname: IUserDAL
* Created: 2018-03-20 19:36:23
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.Redis.WebApi.Mis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.IDAL.Redis.WebApi.Mis
{
    public interface IUserDAL<T> : IBaseEntityDAL<User>
    {
        List<User> GetListByGroupId(int userGroupId);
        List<User> GetListByGroupIds(int[] userGroupIds);
    }
}

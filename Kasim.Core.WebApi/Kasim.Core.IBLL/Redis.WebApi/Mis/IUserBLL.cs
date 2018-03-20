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

* Filename: IUserBLL
* Namespace: Kasim.Core.IBLL.Redis.WebApi.Mis
* Classname: IUserBLL
* Created: 2018-03-20 19:38:44
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.Redis.WebApi.Mis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.IBLL.Redis.WebApi.Mis
{
    public interface IUserBLL
    {
        List<User> GetUsersByGroup(int userGroupId);
        List<User> GetUsersByGroups(int[] userGroupIds);
        User GetUser(int userId);
    }
}

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

* Filename: IUserActionBLL
* Namespace: Kasim.Core.IBLL.WeiXin.MP.WebApp
* Classname: IUserActionBLL
* Created: 2018-05-04 10:10:31
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.Weixin.MP;

namespace Kasim.Core.IBLL.WeiXin.MP.WebApp
{
    public interface IUserActionBLL
    {
        List<UserAction> GetUserActionListByKeyWord(string keyWord);
        int VerifyUserAction(UserAction userAction);
    }
}

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

* Filename: IUserActionDAL
* Namespace: Kasim.Core.IDAL.WeiXin.MP.WebApp
* Classname: IUserActionDAL
* Created: 2018-05-04 10:56:59
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.Weixin.MP;

namespace Kasim.Core.IDAL.WeiXin.MP.WebApp
{
    public interface IUserActionDAL
    {
        List<UserAction> GetListByKeyWord(string keyWord);
        int VerifyUserAction(UserAction userAction);
    }
}

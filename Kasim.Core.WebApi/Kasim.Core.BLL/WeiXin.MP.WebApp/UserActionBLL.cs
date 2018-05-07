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

* Filename: UserActionBLL
* Namespace: Kasim.Core.BLL.WeiXin.MP.WebApp
* Classname: UserActionBLL
* Created: 2018-05-04 10:10:47
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.WeiXin.MP.WebApp;
using Kasim.Core.IDAL.WeiXin.MP.WebApp;
using Kasim.Core.Model.Weixin.MP;
using Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.WeiXin.MP.WebApp
{
    public class UserActionBLL : IUserActionBLL
    {
        IUserActionDAL dal = new UserActionDAL();
        public List<UserAction> GetUserActionListByKeyWord(string keyWord)
        {
            return dal.GetListByKeyWord(keyWord);
        }

        public int VerifyUserAction(UserAction userAction)
        {
            return dal.VerifyUserAction(userAction);
        }
    }
}

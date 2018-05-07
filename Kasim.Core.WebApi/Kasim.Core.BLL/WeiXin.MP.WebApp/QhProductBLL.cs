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

* Filename: QhProductBLL
* Namespace: Kasim.Core.BLL.WeiXin.MP.WebApp
* Classname: QhProductBLL
* Created: 2018-05-04 10:02:09
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
    public class QhProductBLL : IQhProductBLL
    {
        IQhProductDAL dal = new QhProductDAL();
        public List<QhProduct> GetQhProducts()
        {
            return dal.GetList();
        }
    }
}

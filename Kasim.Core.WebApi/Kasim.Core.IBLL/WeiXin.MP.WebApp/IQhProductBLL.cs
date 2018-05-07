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

* Filename: IQhProductBLL
* Namespace: Kasim.Core.IBLL.WeiXin.MP.WebApp
* Classname: IQhProductBLL
* Created: 2018-05-04 10:01:54
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
    public interface IQhProductBLL
    {
        List<QhProduct> GetQhProducts();
    }
}

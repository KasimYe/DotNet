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

* Filename: NewsInfoBLL
* Namespace: Kasim.Core.BLL.WeiXin.MP.WebApp
* Classname: NewsInfoBLL
* Created: 2018-05-04 10:33:21
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
    public class NewsInfoBLL : INewsInfoBLL
    {
        INewsInfoDAL dal = new NewsInfoDAL();
        public bool CheckSendNews(NewsInfo newsInfo)
        {
            return dal.CheckSendNews(newsInfo);
        }

        public int SetSendNewsInfo(NewsInfo newsInfo)
        {
            return dal.SetSendNewsInfo(newsInfo);
        }
    }
}

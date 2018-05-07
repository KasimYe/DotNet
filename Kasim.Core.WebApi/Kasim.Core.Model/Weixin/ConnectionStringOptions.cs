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

* Filename: ConnectionStringOptions
* Namespace: Kasim.Core.Model.Weixin
* Classname: ConnectionStringOptions
* Created: 2018-04-28 14:29:35
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Weixin
{
    public class ConnectionStringOptions
    {
        /// <summary>
        /// ERP数据库
        /// </summary>
        public string DevConnection { get; set; }
        /// <summary>
        /// 税票接口中间库
        /// </summary>
        public string TaxConnection { get; set; }
        /// <summary>
        /// B2B数据库
        /// </summary>
        public string B2bConnection { get; set; }
        /// <summary>
        /// 微信权限数据库
        /// </summary>
        public string DefaultConnection { get; set; }
    }
}

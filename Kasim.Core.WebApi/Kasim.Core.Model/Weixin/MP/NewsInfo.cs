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

* Filename: NewsInfo
* Namespace: Kasim.Core.Model.Weixin.MP
* Classname: NewsInfo
* Created: 2018-05-04 10:31:51
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Weixin.MP
{
    public class NewsInfo
    {
        public int Id { get; set; }
        public string NewsType { get; set; }
        public string PrimaryKey { get; set; }
        public DateTime? NewsDate { get; set; }
        public int SendCount { get; set; }
        public string OpenId { get; set; }
        public bool ReSend { get; set; }
        public DateTime SendTime { get; set; }

    }
}

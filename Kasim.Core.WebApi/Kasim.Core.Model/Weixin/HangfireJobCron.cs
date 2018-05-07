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

* Filename: HangfireJobCron
* Namespace: Kasim.Core.Model.Weixin
* Classname: HangfireJobCron
* Created: 2018-04-28 14:32:32
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Weixin
{
    public class HangfireJobCron
    {
        /// <summary>
        /// 华源当日集单通知时间
        /// </summary>
        public string HYOrder { get; set; }

        /// <summary>
        /// 缺货到货通知时间
        /// </summary>
        public string QHProduct { get; set; }

        /// <summary>
        /// 税票开出通知时间
        /// </summary>
        public string TaxBill { get; set; }

        /// <summary>
        /// 出库状态通知时间
        /// </summary>
        public string SaleBill { get; set; }

    }
}

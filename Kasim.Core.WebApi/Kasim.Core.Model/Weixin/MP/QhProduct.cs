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

* Filename: QhProduct
* Namespace: Kasim.Core.Model.Weixin.MP.WebApp
* Classname: QhProduct
* Created: 2018-05-04 10:04:50
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Weixin.MP
{
    public class QhProduct
    {
        public int Id { get; set; }
        public DateTime SystemDate { get; set; }
        public string ClientName { get; set; }
        public string PName { get; set; }
        public string Model { get; set; }
        public string FromPlace { get; set; }
        public decimal OrderQty { get; set; }
        public DateTime InDate { get; set; }
        public string SalesName { get; set; }
        public decimal InQty { get; set; }
    }
}

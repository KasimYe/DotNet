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

* Filename: TaxBill
* Namespace: Kasim.Core.Model.Weixin.MP
* Classname: TaxBill
* Created: 2018-05-04 14:47:59
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Weixin.MP
{
    public class TaxBill
    {
        public string FormNumber { get; set; }
        public string ClientName { get; set; }
        public string TaxFphm { get; set; }
        public string TaxFpdm { get; set; }
        public DateTime TaxFprq { get; set; }
        public string Note { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal NoTaxTotal { get; set; }
        public decimal PuTotal { get; set; }
    }
}

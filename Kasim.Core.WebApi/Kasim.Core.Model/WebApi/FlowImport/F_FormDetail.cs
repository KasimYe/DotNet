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

* Filename: F_FormDetail
* Namespace: Kasim.Core.Model.WebApi.FlowImport
* Classname: F_FormDetail
* Created: 2018-05-10 21:40:59
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.FlowImport
{
    public class F_FormDetail
    {
        public int FDetailID { get; set; }
        public int FID { get; set; }
        public int PID { get; set; }
        public int RelatedID { get; set; }
        public string Batch { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Quantity2 { get; set; }
        public decimal Quantity3 { get; set; }
        public int GainLossReason { get; set; }
        public decimal InPrice { get; set; }
        public decimal WPrice { get; set; }
        public decimal RPrice { get; set; }
        public decimal NoTaxPrice { get; set; }
        public decimal TaxPrice { get; set; }
        public decimal NoTaxTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal PaidNoTaxTotal { get; set; }
        public decimal PaidTaxTotal { get; set; }
        public int ReviewerID { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int? WarehouseID3 { get; set; }
        public string Place { get; set; }
        public int? Checked { get; set; }
        public int? SubStatus { get; set; }
        public string Note { get; set; }
        public decimal? MDPrice { get; set; }
    }
}

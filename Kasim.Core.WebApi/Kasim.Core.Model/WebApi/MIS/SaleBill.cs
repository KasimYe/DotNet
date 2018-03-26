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

* Filename: SaleBill
* Namespace: Kasim.Core.Model.WebApi.MIS
* Classname: SaleBill
* Created: 2018-03-26 21:32:04
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.MIS
{
    public class SaleBill : BaseEntity
    {
        public int DetailID { get; set; }
        public int SaleBillID { get; set; }
        public string FormNumber { get; set; }
        public int FormTypeID { get; set; }
        public string FormTypeName { get; set; }
        public int StoreID { get; set; }
        public int ClientID { get; set; }
        public int CreatorID { get; set; }
        public int SalesID { get; set; }
        public decimal NoTaxSum { get; set; }
        public decimal TaxSum { get; set; }
        public DateTime SystemDate { get; set; }
        public DateTime SystemTime { get; set; }
        public string TransAddress { get; set; }
        public decimal Quantity { get; set; }
        public decimal Quantity3 { get; set; }
        public decimal TaxPrice { get; set; }
        public decimal NoTaxPrice { get; set; }
        public decimal MDPrice { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal NoTaxTotal { get; set; }
        public string Batch { get; set; }
        public string ExpiryDate { get; set; }
        public string ProductDate { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string CreatorName { get; set; }
        public string SalesName { get; set; }
    }
}

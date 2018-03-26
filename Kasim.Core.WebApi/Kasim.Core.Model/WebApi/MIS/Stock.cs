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

* Filename: Stock
* Namespace: Kasim.Core.Model.WebApi.MIS
* Classname: Stock
* Created: 2018-03-26 21:29:53
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.MIS
{
    public class Stock : BaseEntity
    {
        public int DetailID { get; set; }
        public int StockID { get; set; }
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
        public decimal Quantity { get; set; }
        public decimal TaxPrice { get; set; }
        public decimal NoTaxPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal NoTaxTotal { get; set; }
        public decimal PaidNoTaxTotal { get; set; }
        public decimal PaidTaxTotal { get; set; }
        public string Batch { get; set; }
        public string ExpiryDate { get; set; }
        public string ProductDate { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string CreatorName { get; set; }
    }
}

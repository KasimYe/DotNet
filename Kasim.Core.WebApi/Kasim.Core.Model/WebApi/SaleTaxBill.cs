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

* Filename: SaleTaxBill
* Namespace: Kasim.Core.Model.WebApi
* Classname: SaleTaxBill
* Created: 2018-03-26 12:46:03
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi
{
    public class SaleTaxBill:BaseEntity
    {
        public int BillID { get; set; }
        public string FormNumber { get; set; }
        public int ClientID { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public DateTime SystemDate { get; set; }
        public string FormTypeName { get; set; }
        public string StatusTypeName { get; set; }
        public string FPHM { get; set; }
        public string FPDM { get; set; }
        public DateTime KPRQ { get; set; }
        public decimal TaxPaidSum { get; set; }
        public string Notes { get; set; }
    }
}

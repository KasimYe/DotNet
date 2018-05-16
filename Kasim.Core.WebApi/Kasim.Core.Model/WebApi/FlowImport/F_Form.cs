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

* Filename: F_Form
* Namespace: Kasim.Core.Model.WebApi.FlowImport
* Classname: F_Form
* Created: 2018-05-10 21:36:11
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.FlowImport
{
    public class F_Form
    {
        public int FID { get; set; }
        public int OLDFID { get; set; }
        public int StoreID { get; set; }
        public int FormTypeID { get; set; }
        public Int64 FormNumber { get; set; }
        public int RelatedFID { get; set; }
        public string ContractNumber { get; set; }
        public int ClientID { get; set; }
        public int WarehouseID { get; set; }
        public int WarehouseID2 { get; set; }
        public int CreatorID { get; set; }
        public int SalesID { get; set; }
        public decimal NoTaxSum { get; set; }
        public decimal TaxSum { get; set; }
        public decimal NoTaxPaidSum { get; set; }
        public decimal TaxPaidSum { get; set; }
        public int PayMethod { get; set; }
        public DateTime? FirstPayDate { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public int ReviewerID { get; set; }
        public DateTime SystemDate { get; set; }
        public DateTime SystemTime { get; set; }
    }
}

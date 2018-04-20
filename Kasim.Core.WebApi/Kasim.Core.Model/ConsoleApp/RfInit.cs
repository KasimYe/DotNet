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

* Filename: RfInit
* Namespace: Kasim.Core.Model.ConsoleApp
* Classname: RfInit
* Created: 2018-04-20 20:02:42
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.ConsoleApp
{
    public class Ri_Inventory_ERP
    {
        public int PID { get; set; }        
        public string Batch { get; set; }
        public string ExpiryDate { get; set; }
        public DateTime? ExpiryDate2 { get; set; }
        public string ProductDate { get; set; }
        public DateTime? ProductDate2 { get; set; }
        public decimal Quantity { get; set; }
    }
    public class Ri_Product
    {
        public int PID { get; set; }
        public int RPID { get; set; }
        public string PCode { get; set; }
        public string BarCode { get; set; }
        public string PName { get; set; }
        public string PYName { get; set; }
        public string UnitType { get; set; }
        public string Alias { get; set; }
        public string Model { get; set; }
        public string FromPlace { get; set; }
        public string ShortFromPlace { get; set; }
        public string BaseUnit { get; set; }
        public decimal UnitRate { get; set; }
        public int Status { get; set; }
        public int WareID { get; set; }
        public bool IsDrug { get; set; }
        public string WTFromPlace { get; set; }
        public bool SRFH { get; set; }
        public decimal MidUnitRate { get; set; }

    }
}

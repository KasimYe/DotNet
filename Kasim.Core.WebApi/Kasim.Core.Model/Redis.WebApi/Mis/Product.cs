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

* Filename: Product
* Namespace: Kasim.Core.Model.Redis.WebApi.Mis
* Classname: Product
* Created: 2018-03-20 21:51:56
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.Redis.WebApi.Mis
{
    public class Product : BaseEntity
    {
        public int PID { get; set; }
        public string PCode { get; set; }
        public string PName { get; set; }
        public string PYName { get; set; }
        public string UnitType { get; set; }
        public string Alias { get; set; }
        public string Model { get; set; }
        public string FromPlace { get; set; }
        public string ShortFromPlace { get; set; }
        public decimal PuRate { get; set; }
        public string BaseUnit { get; set; }
        public decimal UnitRate { get; set; }
        public decimal MidUnitRate { get; set; }
        public decimal MinUnitRate { get; set; }
        public decimal LSPrice { get; set; }
        public decimal PFPrice { get; set; }
        public string BrTaxCode { get; set; }
        public int Status { get; set; }
    }
}

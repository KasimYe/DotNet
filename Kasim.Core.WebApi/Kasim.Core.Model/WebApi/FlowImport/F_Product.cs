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

* Filename: F_Product
* Namespace: Kasim.Core.Model.WebApi.FlowImport
* Classname: F_Product
* Created: 2018-05-07 21:22:05
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.FlowImport
{
    public class F_Product
    {
        public int PID { get; set; }
        public long PCode5 { get; set; }
        public string PName { get; set; }
        public string PYName { get; set; }
        public string UnitType { get; set; }
        public string Alias { get; set; }
        public string Model { get; set; }
        public string FromPlace { get; set; }
        public string ShortFromPlace { get; set; }
        public decimal PuRate { get; set; }
        public int PTypeID { get; set; }
        public string BaseUnit { get; set; }
        public string UnitRate { get; set; }
        public int Status { get; set; }
        public string MUnitRate { get; set; }
        public int LastClientId { get; set; }
    }
}

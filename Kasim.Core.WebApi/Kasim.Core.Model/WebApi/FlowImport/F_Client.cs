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

* Filename: F_Client
* Namespace: Kasim.Core.Model.WebApi.FlowImport
* Classname: F_Client
* Created: 2018-05-10 21:31:54
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.FlowImport
{
    public class F_Client
    {
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string PYName { get; set; }
        public bool BottomLevel { get; set; }
        public int ClientType { get; set; }
        public bool BCustomer { get; set; }
        public bool BSupplier { get; set; }
        public int Status { get; set; }
    }
}

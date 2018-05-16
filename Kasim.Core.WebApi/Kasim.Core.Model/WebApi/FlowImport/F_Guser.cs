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

* Filename: F_Guser
* Namespace: Kasim.Core.Model.WebApi.FlowImport
* Classname: F_Guser
* Created: 2018-05-15 14:20:35
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.WebApi.FlowImport
{
    public class F_Guser
    {
        public int UserID { get; set; }
        public int UserCode { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int UserGroupID { get; set; }
        public int StoreID { get; set; }
        public int DID { get; set; }
        public string Telephone { get; set; }
        public string ThePost { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? WorkTime { get; set; }
        public DateTime? CallTime { get; set; }
        public char PersonSex { get; set; }
        public string PersonEdu { get; set; }
        public string PersonProf { get; set; }
        public string Qualification { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public bool Super { get; set; }
        public bool StoreType { get; set; }
    }
}

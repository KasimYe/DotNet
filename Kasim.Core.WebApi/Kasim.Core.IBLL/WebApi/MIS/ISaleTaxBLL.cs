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

* Filename: ISaleTaxBLL
* Namespace: Kasim.Core.IBLL.WebApi
* Classname: ISaleTaxBLL
* Created: 2018-03-26 12:44:11
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.WebApi.MIS;
using System;
using System.Collections.Generic;

namespace Kasim.Core.IBLL.WebApi.MIS
{
    public interface ISaleTaxBLL
    {
        List<SaleTaxBill> GetSaleTaxBills(DateTime startDate, DateTime endDate, int clientId, int taxTypeId = -1, int taxStatusId = -1,string formNumber="", int pId = -1);
    }
}

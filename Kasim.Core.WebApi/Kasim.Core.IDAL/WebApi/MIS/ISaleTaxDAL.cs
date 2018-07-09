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

* Filename: ISaleTaxDAL
* Namespace: Kasim.Core.IDAL.WebApi
* Classname: ISaleTaxDAL
* Created: 2018-03-26 13:12:16
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.WebApi.MIS;
using System;
using System.Collections.Generic;

namespace Kasim.Core.IDAL.WebApi.MIS
{
    public interface ISaleTaxDAL<T> : IBaseDAL<SaleTaxBill>
    {
        List<SaleTaxBill> GetList(DateTime startDate, DateTime endDate, int clientId, int taxTypeId=-1, int taxStatusId=-1, string formNumber = "", int pId=-1);
        List<SaleTaxBill> GetListMskl(DateTime startDate, DateTime endDate, int clientId, int taxTypeId, int taxStatusId, string formNumber, int pId);
    }
}

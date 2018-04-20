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

* Filename: IRfInitDAL
* Namespace: Kasim.Core.IDAL.ConsoleApp
* Classname: IRfInitDAL
* Created: 2018-04-20 20:06:59
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.ConsoleApp;

namespace Kasim.Core.IDAL.ConsoleApp
{
    public interface IRfInitDAL
    {
        List<Ri_Inventory_ERP> GetInventoryFromErp();
        int InsertStockInit();
        int ValProducts(int pId);
        Ri_Product GetProductByIDFromErp(int pID);
        int InsertProduct(Ri_Product product);
        int GetBatchId(int wmsPid, Ri_Inventory_ERP inv);
        int InsertStockDetailInit(int stockId, int wmsPbid, decimal quantity);
    }
}

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

* Filename: IFlowImportDAL
* Namespace: Kasim.Core.IDAL.WebApi
* Classname: IFlowImportDAL
* Created: 2018-05-07 21:49:58
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.WebApi.FlowImport;

namespace Kasim.Core.IDAL.WebApi
{
    public interface IFlowImportDAL
    {
        int AddProduct(F_Product entity);
        int TruncateProducts();
        int AddClient(F_Client entity);
        int AddForm(F_Form entity);
        int AddFormDetail(F_FormDetail entity);
        int AddInv(F_Inv entity);
        int SetFormStatus(int status);
        int TruncateClients();
        int TruncateInvs();
        int AddClientPriceType(F_ClientPriceType entity);
        int AddGuser(F_Guser entity);
        int AddProductPrice(F_ProductPrice entity);
        int TruncateClientPriceTypes();
        int TruncateGusers();
        int TruncateProductPrices();
    }
}

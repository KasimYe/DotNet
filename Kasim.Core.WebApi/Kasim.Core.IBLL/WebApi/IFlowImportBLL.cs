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

* Filename: IFlowImportBLL
* Namespace: Kasim.Core.IBLL.WebApi
* Classname: IFlowImportBLL
* Created: 2018-05-07 21:49:13
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.WebApi.FlowImport;

namespace Kasim.Core.IBLL.WebApi
{
    public interface IFlowImportBLL
    {
        int AddProduct(F_Product entity);
        int TruncateProducts();
        int TruncateClients();
        int AddClient(F_Client entity);
        int TruncateInvs();
        int AddInv(F_Inv entity);
        int SetFormStatus(int status);
        int AddForm(F_Form entity);
        int AddFormDetail(F_FormDetail entity);
        int TruncateProductPrices();
        int AddProductPrice(F_ProductPrice entity);
        int TruncateClientPriceTypes();
        int AddClientPriceType(F_ClientPriceType entity);
        int TruncateGusers();
        int AddGuser(F_Guser entity);
    }
}

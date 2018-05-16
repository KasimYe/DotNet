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

* Filename: FlowImportBLL
* Namespace: Kasim.Core.BLL.WebApi
* Classname: FlowImportBLL
* Created: 2018-05-07 21:48:52
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.WebApi;
using Kasim.Core.IDAL.WebApi;
using Kasim.Core.Model.WebApi.FlowImport;
using Kasim.Core.SQLServerDAL.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.WebApi
{
    public class FlowImportBLL : IFlowImportBLL
    {
        IFlowImportDAL dal = new FlowImportDAL();

        public int AddClient(F_Client entity) => dal.AddClient(entity);

        public int AddClientPriceType(F_ClientPriceType entity) => dal.AddClientPriceType(entity);

        public int AddForm(F_Form entity) => dal.AddForm(entity);

        public int AddFormDetail(F_FormDetail entity) => dal.AddFormDetail(entity);

        public int AddGuser(F_Guser entity) => dal.AddGuser(entity);

        public int AddInv(F_Inv entity) => dal.AddInv(entity);

        public int AddProduct(F_Product entity) => dal.AddProduct(entity);

        public int AddProductPrice(F_ProductPrice entity) => dal.AddProductPrice(entity);

        public int SetFormStatus(int status) => dal.SetFormStatus(status);

        public int TruncateClientPriceTypes() => dal.TruncateClientPriceTypes();

        public int TruncateClients() => dal.TruncateClients();

        public int TruncateGusers() => dal.TruncateGusers();

        public int TruncateInvs() => dal.TruncateInvs();

        public int TruncateProductPrices() => dal.TruncateProductPrices();

        public int TruncateProducts() => dal.TruncateProducts();
    }
}

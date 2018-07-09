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

* Filename: SaleTaxBLL
* Namespace: Kasim.Core.BLL.WebApi
* Classname: SaleTaxBLL
* Created: 2018-03-26 12:44:28
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.WebApi.MIS;
using Kasim.Core.IDAL.WebApi.MIS;
using Kasim.Core.Model.WebApi;
using Kasim.Core.Model.WebApi.MIS;
using Kasim.Core.SQLServerDAL.WebApi.MIS;
using System;
using System.Collections.Generic;

namespace Kasim.Core.BLL.WebApi.MIS
{
    public class SaleTaxBLL: ISaleTaxBLL
    {
        ISaleTaxDAL<SaleTaxBill> dal;
        public SaleTaxBLL(ConnectionStringOptions conns)
        {
            dal = new SaleTaxDAL(conns);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="clientId"></param>
        /// <param name="taxTypeId">1为普通发票，2为专用发票</param>
        /// <param name="taxStatusId">1为纸质发票，11为电子发票</param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<SaleTaxBill> GetSaleTaxBills(DateTime startDate, DateTime endDate, int clientId, int taxTypeId = -1, int taxStatusId = -1, string formNumber = "", int pId = -1)
        {
            if (endDate >= DateTime.Now.Date) endDate = DateTime.Now.AddDays(-1).Date;
            var list = dal.GetListMskl(startDate, endDate, clientId, taxTypeId, taxStatusId, formNumber, pId);
            var list2= dal.GetList(startDate, endDate, clientId, taxTypeId, taxStatusId, formNumber, pId);
            list.AddRange(list2);
            return list;
        }
    }
}

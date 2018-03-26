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

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.IDAL.WebApi;
using Kasim.Core.Model.WebApi;
using Kasim.Core.SQLServerDAL.WebApi;

namespace Kasim.Core.BLL.WebApi
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
            return dal.GetList(startDate, endDate, clientId, taxTypeId, taxStatusId, formNumber, pId);
        }
    }
}

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

* Filename: RfInitBLL
* Namespace: Kasim.Core.BLL.ConsoleApp
* Classname: RfInitBLL
* Created: 2018-04-20 19:57:28
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.ConsoleApp;
using Kasim.Core.SQLServerDAL.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class RfInitBLL : IRfInitBLL
    {
        IRfInitDAL dal = new RfInitDAL();
        public void InitStock()
        {
            try
            {
                var invList = GetInventoryFromErp();
                Console.WriteLine(string.Format("获取库存成功,共{0}条,按任意键继续", invList.Count));
                Console.ReadKey();
                int stockId = InsertStock();
                Console.WriteLine(string.Format("初始化入库单据头生成成功,ID:{0},按任意键继续", stockId));
                Console.ReadKey();
                invList.ForEach(i =>
                {
                    int wmsPid = ValProducts(i.PID);
                    if (wmsPid == 0)
                    {
                        var product = GetProductByIDFromErp(i.PID);
                        wmsPid = InsertProduct(product);
                        Console.WriteLine(string.Format("{0}:不存在,新增成功!ID:{1}", product.PName, wmsPid));
                    }
                    int wmsPbid = GetBatchId(wmsPid, i);
                    Console.WriteLine(string.Format("获取批号ID:{0}", wmsPbid));
                    int ret = InsertStockDetail(stockId, wmsPbid, i.Quantity);
                    Console.WriteLine(string.Format("初始化入库单据明细生成结果:{0}", ret));
                });
                Console.WriteLine("初始化入库单生成结束,按任意键继续");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception:{0}", ex.Message));
                Console.ReadKey();
            }
            
        }

        private int InsertStockDetail(int stockId, int wmsPbid, decimal quantity) => dal.InsertStockDetailInit(stockId, wmsPbid, quantity);

        private int GetBatchId(int wmsPid, Ri_Inventory_ERP inv) => dal.GetBatchId(wmsPid, inv);

        private int InsertProduct(Ri_Product product) => dal.InsertProduct(product);

        private Ri_Product GetProductByIDFromErp(int pID) => dal.GetProductByIDFromErp(pID);

        private int ValProducts(int pId) => dal.ValProducts(pId);

        private int InsertStock() => dal.InsertStockInit();

        private List<Ri_Inventory_ERP> GetInventoryFromErp() => dal.GetInventoryFromErp();
    }
}

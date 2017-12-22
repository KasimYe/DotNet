/**
 *                             _ooOoo_
 *                            o8888888o
 *                            88" . "88
 *                            (| -_- |)
 *                            O\  =  /O
 *                         ____/`---'\____
 *                       .'  \\|     |//  `.
 *                      /  \\|||  :  |||//  \
 *                     /  _||||| -:- |||||-  \
 *                     |   | \\\  -  /// |   |
 *                     | \_|  ''\---/''  |   |
 *                     \  .-\__  `-`  ___/-. /
 *                   ___`. .'  /--.--\  `. . __
 *                ."" '<  `.___\_<|>_/___.'  >'"".
 *               | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *               \  \ `-.   \_ __\ /__ _/   .-` /  /
 *          ======`-.____`-.___\_____/___.-`____.-'======
 *                             `=---='
 *          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
 *                     佛祖保佑        永无BUG
 *            佛曰:
 *                   写字楼里写字间，写字间里程序员；
 *                   程序人员写程序，又拿程序换酒钱。
 *                   酒醒只在网上坐，酒醉还来网下眠；
 *                   酒醉酒醒日复日，网上网下年复年。
 *                   但愿老死电脑间，不愿鞠躬老板前；
 *                   奔驰宝马贵者趣，公交自行程序员。
 *                   别人笑我忒疯癫，我笑自己命太贱；
 *                   不见满街漂亮妹，哪个归得程序员？
*/
/*----------------------------------------------------------------
** Copyright (C) 2017 
**
** file：SupplierReturnBLL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2017-12-22 14:59:14
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Kasim.Core.Factory;
using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.ConsoleApp;
using Kasim.Core.Model.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class SupplierReturnBLL : ISupplierReturnBLL
    {
        ISupplierReturnDAL supplierReturnDAL = DALFactory.CreateSupplierReturnDAL();

        public SupplierReturnBLL(ConnectionStringOptions connectionStrings)
        {
            ConnectionFactory.ConnectionString = connectionStrings.DevConnection;
        }

        public void SupplySupplierReturn(int sRSCID)
        {
            var keyCode = sRSCID.ToString();
            while (keyCode.ToUpper() != "BACK")
            {
                var supplierReturnSaleClient = supplierReturnDAL.GetEntity(int.Parse(keyCode));
                if (supplierReturnSaleClient != null)
                {
                    var outMsg = string.Format("产品：{0}\r\n起始日期：{1}\r\n截至日期：{2}\r\n起始单价：{3}\r\n截至单价：{4}\r\n起始数量：{5}\r\n截至数量：{6}\r\n返利价格：{7}\r\n是否开始查询遗漏销售明细返利？(Y/N)",
                    supplierReturnSaleClient.PName, supplierReturnSaleClient.StartDate.ToShortDateString(), supplierReturnSaleClient.EndDate.ToShortDateString(),
                    supplierReturnSaleClient.MinPrice.ToString("0.00#"), supplierReturnSaleClient.MaxPrice.ToString("0.00#"), supplierReturnSaleClient.MinQuantity.ToString("0.##"), supplierReturnSaleClient.MaxQuantity.ToString("0.##"),
                    supplierReturnSaleClient.SupplierReturn.ToString("0.00##"));
                    Console.WriteLine(outMsg);
                    var contKey = Console.ReadKey();
                    if (contKey.Key == ConsoleKey.Y)
                    {
                        SearchSaleBillDetail(supplierReturnSaleClient);
                    }
                    else
                    {
                        Console.WriteLine("请输入返利设置ID：");
                    }
                }
                else
                {
                    Console.WriteLine("返利不存在,请输入正确的返利ID");
                }
                keyCode = Console.ReadLine();
            }
        }

        private void SearchSaleBillDetail(SupplierReturnSaleClients entity)
        {

            var list = supplierReturnDAL.GetSaleBillDetailCount(entity);
            if (list != null && list.Count > 0)
            {
                var keyCode = Console.ReadLine();
                while (keyCode.ToUpper() != "BACK")
                {
                    var writeMsg = string.Format("共找到未关联返利明细{0}条(包含不符合条件)\r\n是否开始执行检查并关联操作(Y/N)", list.Count);
                    Console.WriteLine(writeMsg);
                    var contKey = Console.ReadKey();
                    if (contKey.Key == ConsoleKey.Y)
                    {
                        CheckSupplierReturn(list);
                    }
                    else
                    {
                        break;
                    }
                    keyCode = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("找不到未关联返利的销售明细!");
            }
            Console.WriteLine("请输入返利设置ID：");
        }

        private void CheckSupplierReturn(List<SaleBillDetail> list)
        {
            SaleBillDetail curMsg = null;
            try
            {
                foreach (var entity in list)
                {                    
                    curMsg = entity;
                    var saleBillDetail = supplierReturnDAL.CheckSupplierReturn(entity);
                    if (saleBillDetail != null && saleBillDetail.SRSCID != null && saleBillDetail.SRSCID != 0)
                    {
                        Console.WriteLine(string.Format("符合返利条件：\r\n日期:{0} 客户:{1} 单号:{2} 批号:{3} 数量:{4} 单价:{5}",
                            entity.SystemDate.ToShortDateString(), entity.ClientName, entity.FormNumber, entity.Batch,
                            entity.Quantity.ToString("0.###"), entity.TaxPrice.ToString("0.00##")));
                    }
                    else
                    {
                        Console.WriteLine("不符合返利条件");
                    }
                    //System.Threading.Thread.Sleep(100);
                }
                Console.WriteLine("关联执行完毕");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\r\n$$$$$$$$$$$$$$$$$$$$$$ ERROR $$$$$$$$$$$$$$$$$$$$$$\r\n{0}\r\n{1}\r\n$$$$$$$$$$$$$$$$$$$$$$ ERROR $$$$$$$$$$$$$$$$$$$$$$", 
                    ex.Message,curMsg.ToString());                
            }
        }
    }
}

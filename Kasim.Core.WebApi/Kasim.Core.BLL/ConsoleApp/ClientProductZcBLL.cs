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
** file：ClientProductZcBLL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-02-26 10:49:10
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Kasim.Core.Common;
using Kasim.Core.Factory;
using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Kasim.Core.Model.WebApi;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class ClientProductZcBLL : IClientProductZcBLL
    {
        IClientProductZcDAL<ClientProductZc> dal = DALFactory<IClientProductZcDAL<ClientProductZc>>.CreateDAL("Kasim.Core.SQLServerDAL", "ConsoleApp.ClientProductZcDAL");
        public ClientProductZcBLL(ConnectionStringOptions connectionStringOptions)
        {
            ConnectionFactory.ConnectionString = connectionStringOptions.DevConnection;
        }

        public List<ClientProductZc> GetListFromExcel(string filePath,int storeId,int pid,decimal cost)
        {
            IClientDAL<Client> dal = DALFactory<IClientDAL<Client>>.CreateDAL("Kasim.Core.SQLServerDAL", "ConsoleApp.ClientDAL");
            var dt = ExcelExport.GetDataTable(filePath, 1);
            var list = new List<ClientProductZc>();
            foreach (DataRow dr in dt.Rows)
            {
                var entity = dal.GetEntityByCode(dr["客户编码"].ToString());
                if (list!=null && list.Where(x => x.ClientId == entity.ClientId).ToList().Count == 0)
                {
                    list.Add(new ClientProductZc
                    {
                        StoreId = storeId,
                        PId = pid,
                        ClientId = entity.ClientId,
                        ZContent = cost,
                    });
                }                
            }
            Console.WriteLine(string.Format("客户[{0}]家,StoreID={1},PID={2},考核成本[{3}]",list.Count,storeId,pid,cost));
            return list;
        }

        public bool ImportList(List<ClientProductZc> list)
        {
            try
            {
                foreach (var entity in list)
                {
                    dal.Insert(entity);
                }
                Console.WriteLine("导入完毕");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }
    }
}

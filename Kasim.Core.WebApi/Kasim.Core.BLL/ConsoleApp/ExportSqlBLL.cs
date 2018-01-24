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
** file：ExportSql
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-01-24 13:10:38
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Kasim.Core.Common;
using Kasim.Core.Factory;
using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.WebApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class ExportSqlBLL : IExportSqlBLL
    {
        IExportSqlDAL dalMySql = DALFactory<IExportSqlDAL>.CreateDAL("Kasim.Core.MySQLDAL", "ConsoleApp.ExportSqlDAL");
        IExportSqlDAL dalSqlServer = DALFactory<IExportSqlDAL>.CreateDAL("Kasim.Core.SQLServerDAL", "ConsoleApp.ExportSqlDAL");

        public ExportSqlBLL(ConnectionStringOptions connectionStrings)
        {
            ConnectionFactory.ConnectionString = connectionStrings.DevConnection;
        }

        public void ExportByDataTable(string workSheetName, DataTable dataTable)
        {
            ExcelExport.ExportByDataTable(dataTable, workSheetName);
            Console.WriteLine("导出成功");
        }

        public void ExportByMySql(string workSheetName, string sql)
        {
            var dt = dalMySql.GetDataTable(sql);
            ExportByDataTable(workSheetName,dt);
        }

        public void ExportBySqlServer(string workSheetName, string sql)
        {
            Console.WriteLine("…数据查询中…");
            var dt = dalSqlServer.GetDataTable(sql);
            if (dt.Rows.Count>1048575)
            {
                Console.WriteLine(string.Format("…数据记录为{0},超过了Excel最大数据行1048576,自动分割请输入命令…", dt.Rows.Count));
                var goCode = Console.ReadLine();
                if (goCode.ToUpper() == "YES")
                {
                    ExportByDataTable(workSheetName, dt);
                }
            }
            else
            {
                ExportByDataTable(workSheetName, dt);
            }
            
            
        }

        
    }
}

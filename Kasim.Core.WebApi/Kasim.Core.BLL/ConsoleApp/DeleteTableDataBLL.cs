/*********************************************************************
 **                             _ooOoo_                             **
 **                            o8888888o                            **
 **                            88" . "88                            **
 **                            (| -_- |)                            **
 **                            O\  =  /O                            **
 **                         ____/`---'\____                         **
 **                       .'  \\|     |//  `.                       **
 **                      /  \\|||  :  |||//  \                      **
 **                     /  _||||| -:- |||||-  \                     **
 **                     |   | \\\  -  /// |   |                     **
 **                     | \_|  ''\---/''  |   |                     **
 **                     \  .-\__  `-`  ___/-. /                     **
 **                   ___`. .'  /--.--\  `. . __                    **
 **                ."" '<  `.___\_<|>_/___.'  >'"".                 **
 **               | | :  `- \`.;`\ _ /`;.`/ - ` : | |               **
 **               \  \ `-.   \_ __\ /__ _/   .-` /  /               **
 **          ======`-.____`-.___\_____/___.-`____.-'======          **
 **                             `=---='                             **
 **          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^          **
 **                     佛祖保佑        永无BUG                     **
 **            佛曰:                                                **
 **                   写字楼里写字间，写字间里程序员；              **
 **                   程序人员写程序，又拿程序换酒钱。              **
 **                   酒醒只在网上坐，酒醉还来网下眠；              **
 **                   酒醉酒醒日复日，网上网下年复年。              **
 **                   但愿老死电脑间，不愿鞠躬老板前；              **
 **                   奔驰宝马贵者趣，公交自行程序员。              **
 **                   别人笑我忒疯癫，我笑自己命太贱；              **
 **                   不见满街漂亮妹，哪个归得程序员？              **
 *********************************************************************/
/*=====================================================================
* Copyright (c) 2018 All Rights Reserved.
* CLRVer.:4.0.30319.42000
* machinenameDESKTOP-U288O1H
* namespace:Kasim.Core.BLL.ConsoleApp
* filename:DeleteTableDataBLL
* guid:51e55d26-6d93-4a7d-9bdf-a2e9d7bfe478
* auth:lip86
* date:2018-02-07 19:22:07
* desc:
*
*=====================================================================*/
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
    public class DeleteTableDataBLL : IDeleteTableDataBLL
    {
        IExportSqlDAL dalSql = DALFactory<IExportSqlDAL>.CreateDAL("Kasim.Core.SQLServerDAL", "ConsoleApp.ExportSqlDAL");
        IDeleteTableDataDAL dal = DALFactory<IDeleteTableDataDAL>.CreateDAL("Kasim.Core.SQLServerDAL", "ConsoleApp.DeleteTableDataDAL");

        public DeleteTableDataBLL(ConnectionStringOptions connectionStrings)
        {
            ConnectionFactory.ConnectionString = connectionStrings.TaxConnection;
        }

        public int DeleteTable(string formTable, string detailTable, string primarykey)
        {
            var dt = dalSql.GetDataTable(string.Format("SELECT TOP 10000 {0} FROM {1} ORDER BY {2}", primarykey, formTable, primarykey));
            while (dt.Rows.Count>0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var c = 0;
                    if (!string.IsNullOrEmpty(detailTable))
                    {
                        c = dal.ClearTable(detailTable, primarykey, dr[primarykey].ToString());
                    }
                    dal.ClearTable(formTable, primarykey, dr[primarykey].ToString());
                    Console.WriteLine("删除{0},明细{1}", dr[primarykey], c);
                }
                Console.WriteLine("***** 本次成功删除条目{0} *****", dt.Rows.Count);
                return DeleteTable(formTable, detailTable, primarykey);
            }
            return 0;
        }

        public void DropTable()
        {
            var dt = dalSql.GetDataTable("EXEC dbo.GetTableSpace @columnName = 'data',@sort = 'desc'");
            
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["name"].ToString().ToUpper().Substring(0,2)=="T_")
                {
                    dalSql.ExecSql(string.Format("DROP TABLE {0}",dr["name"]));
                    Console.WriteLine("***** 本次成功删除数据库表名{0} *****", dr["name"]);
                }
            }
        }
    }
}

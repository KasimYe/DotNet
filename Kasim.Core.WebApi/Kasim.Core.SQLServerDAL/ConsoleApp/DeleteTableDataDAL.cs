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
* namespace:Kasim.Core.SQLServerDAL.ConsoleApp
* filename:DeleteTableDataDAL
* guid:113c3d0a-1fa4-4b0a-9dbc-aeecd7d4a8fc
* auth:lip86
* date:2018-02-07 19:34:36
* desc:
*
*=====================================================================*/
using Kasim.Core.Factory;
using Kasim.Core.IDAL.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Kasim.Core.SQLServerDAL.ConsoleApp
{
    public class DeleteTableDataDAL : IDeleteTableDataDAL
    {
        public int ClearTable(string tableName, string primaryKey, string id)
        {
            using (SqlConnection Conn = new SqlConnection(ConnectionFactory.ConnectionString))
            {
                Conn.Open();
                var Comm = new SqlCommand
                {
                    Connection = Conn,
                    CommandTimeout = 10 * 60,
                    CommandType = CommandType.Text,
                    CommandText = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", tableName, primaryKey, id)
                };
                var result = Comm.ExecuteNonQuery();
                Comm.Dispose();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

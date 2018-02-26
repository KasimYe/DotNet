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
** file：ClientProductZcDAL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-02-26 13:36:41
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Dapper;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.ConsoleApp
{
    public class ClientProductZcDAL : IClientProductZcDAL<ClientProductZc>
    {
        public int Delete(ClientProductZc t)
        {
            throw new NotImplementedException();
        }

        public ClientProductZc GetEntity(object id)
        {
            throw new NotImplementedException();
        }

        public ClientProductZc GetEntity(ClientProductZc entity)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT * FROM dbo.ClientProductZC WHERE PID=@PID AND ClientID=@ClientID AND StoreID=@StoreID";
                var result = Conn.Query<ClientProductZc>(query, new { PID = entity.PId, ClientID = entity.ClientId, StoreID = entity.StoreId }).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<ClientProductZc> GetList()
        {
            throw new NotImplementedException();
        }

        public int Insert(ClientProductZc t)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                var param = new DynamicParameters();
                param.Add("@ZContent", t.ZContent, DbType.Decimal);
                param.Add("@PID", t.PId, DbType.Int32);
                param.Add("@ClientID", t.ClientId, DbType.Int32);
                param.Add("@StoreID", t.StoreId, DbType.Int32);
                var result=Conn.Execute("dbo.SetClientProductZC", param, commandType: CommandType.StoredProcedure);           
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int Update(ClientProductZc t)
        {
            throw new NotImplementedException();
        }
    }
}

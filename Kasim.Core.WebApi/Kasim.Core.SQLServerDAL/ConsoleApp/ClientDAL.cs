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
** file：ClientDAL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-02-26 11:27:13
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Dapper;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.Model.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.ConsoleApp
{
    public class ClientDAL : IClientDAL<Client>
    {
        public int Delete(Client t)
        {
            throw new NotImplementedException();
        }

        public Client GetEntity(object id)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT * FROM dbo.Clients WHERE ClientID=@ClientID";
                var result = Conn.Query<Client>(query, new { ClientID = id }).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public Client GetEntityByCode(string clientCode)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT * FROM dbo.Clients WHERE ClientCode=@ClientCode";
                var result = Conn.Query<Client>(query, new { ClientCode = clientCode }).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<Client> GetList()
        {
            throw new NotImplementedException();
        }

        public int Insert(Client t)
        {
            throw new NotImplementedException();
        }

        public int Update(Client t)
        {
            throw new NotImplementedException();
        }
    }
}

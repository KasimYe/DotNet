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
** file：SupplierReturnDAL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2017-12-22 15:05:12
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
    public class SupplierReturnDAL : ISupplierReturnDAL
    {
        public string AddSupplierReturn(SaleBillDetail detail)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                var param = new DynamicParameters();
                param.Add("@SupplierReturn", detail.SupplierReturn, DbType.Decimal);
                param.Add("@DetailID", detail.DetailID, DbType.Int32);
                param.Add("@UserID", 1, DbType.Int32);
                param.Add("@SRSCID", detail.SRSCID, DbType.Int32);
                param.Add("@ret", "", DbType.String, ParameterDirection.Output);
                Conn.Execute("dbo.SupplierReturnSaleClients_ADD", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<string>("@ret");
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public SaleBillDetail CheckSupplierReturn(SaleBillDetail entity)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                var param = new DynamicParameters();
                param.Add("@SystemDate", entity.SystemDate, DbType.DateTime);
                param.Add("@StoreID", 1, DbType.Int32);
                param.Add("@PID", entity.PID, DbType.Int32);
                param.Add("@CostID", entity.CostID, DbType.Int32);
                param.Add("@ClientID", entity.ClientID, DbType.Int32);
                param.Add("@TaxPrice", entity.TaxPrice, DbType.Decimal);
                param.Add("@Quantity", entity.Quantity, DbType.Decimal);
                param.Add("@SupplierReturn", 0, DbType.Decimal, ParameterDirection.Output,36, 18, 4);
                param.Add("@SRSCID", 0, DbType.Int32, ParameterDirection.Output);
                param.Add("@ByPurPay", false, DbType.Boolean, ParameterDirection.Output);
                Conn.Execute("dbo.GetSupplierReturn", param, commandType: CommandType.StoredProcedure);               
                entity.SupplierReturn =param.Get<decimal?>("@SupplierReturn");
                entity.SRSCID = param.Get<int?>("@SRSCID");
                entity.ByPurPay = param.Get<bool?>("@ByPurPay");
                Conn.Close();
                Conn.Dispose();
                return entity;
            }
        }

        public SupplierReturnSaleClients GetEntity(int sRSCID)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT SRSCID ,SID ,ClientID ,StoreID ,PID ,(SELECT PName+'/'+FromPlace+'/'+Model FROM dbo.Products WHERE PID=dbo.SupplierReturnSaleClients.PID) AS PName,ByCostID ,StartDate ,EndDate ,MinPrice ,MaxPrice ,MinQuantity ,MaxQuantity ,SupplierReturn ,SystemDate ,CreatorID ,Status ,SupplierID ,ByPurPay FROM dbo.SupplierReturnSaleClients WHERE SRSCID=@SRSCID";
                var result = Conn.Query<SupplierReturnSaleClients>(query, new { SRSCID = sRSCID }).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<SaleBillDetail> GetSaleBillDetailCount(SupplierReturnSaleClients entity)
        {
            using (var Conn = ConnectionFactory.Connection)
            {
                var query = "SELECT d.DetailID,FormNumber,SystemDate,(SELECT ClientName FROM dbo.Clients WHERE ClientID=f.ClientID) AS ClientName,pb.PID,CostID,f.ClientID,Batch,TaxPrice,Quantity,TaxTotal "
                    + "FROM dbo.SaleBill f,dbo.SaleBillDetail d,dbo.ProductBatches pb WHERE f.SaleBillID=d.SaleBillID AND d.PBID=pb.PBID AND f.SystemDate BETWEEN @StartDate AND @EndDate "
                    + "AND pb.PID=@PID AND ISNULL(f.WarehouseID,-1)<>0 AND d.SRSCID IS NULL AND f.FormTypeID IN (7,8,9) AND StoreID=1";
                var result = Conn.Query<SaleBillDetail>(query, new
                {
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    PID = entity.PID
                }).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

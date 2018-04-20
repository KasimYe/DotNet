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

* Filename: RfInitDAL
* Namespace: Kasim.Core.SQLServerDAL.ConsoleApp
* Classname: RfInitDAL
* Created: 2018-04-20 20:07:18
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

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
    public class RfInitDAL : IRfInitDAL
    {
        public int GetBatchId(int wmsPid, Ri_Inventory_ERP inv)
        {
            ConnectionFactory.ConnectionString = "Data Source=172.168.41.50;database=Bz_WMS;uid=sa;pwd=BRYY@abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                var param = new DynamicParameters();
                param.Add("@PID", wmsPid, DbType.Int32);
                param.Add("@Batch", inv.Batch, DbType.String);
                param.Add("@ExpiryDate", inv.ExpiryDate, DbType.String);
                param.Add("@ExpiryDate2", inv.ExpiryDate2, DbType.DateTime);
                param.Add("@PBID", 0, DbType.Int32, ParameterDirection.Output);
                Conn.Execute("dbo.GetBatchIndex", param, commandType: CommandType.StoredProcedure);
                var result = param.Get<int>("@PBID");
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public List<Ri_Inventory_ERP> GetInventoryFromErp()
        {
            ConnectionFactory.ConnectionString = "Data Source=192.168.116.39;database=Bz_MIS;uid=sa;pwd=abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT pb.PID,pb.Batch,pb.ExpiryDate,pb.ExpiryDate2,pb.ProductDate,pb.ProductDate2,SUM(i.Quantity) AS Quantity FROM dbo.Inventory i,dbo.ProductBatches pb WHERE Quantity>0 AND WarehouseID=1 AND i.PBID=pb.PBID GROUP BY pb.PID,pb.Batch,pb.ExpiryDate,pb.ExpiryDate2,pb.ProductDate,pb.ProductDate2";
                var result = Conn.Query<Ri_Inventory_ERP>(query, null).ToList();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public Ri_Product GetProductByIDFromErp(int pID)
        {
            ConnectionFactory.ConnectionString = "Data Source=192.168.116.39;database=Bz_MIS;uid=sa;pwd=abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT PID AS RPID,PCode,BarCode,PName,PYName,UnitType,Alias,Model,FromPlace,ShortFromPlace,BaseUnit,UnitRate,Status,WareID,IsDrug,WTFromPlace,SRFH,MidUnitRate FROM dbo.Products WHERE PID=@pID";
                var result = Conn.Query<Ri_Product>(query, new { pID }).FirstOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int InsertProduct(Ri_Product product)
        {
            ConnectionFactory.ConnectionString = "Data Source=172.168.41.50;database=Bz_WMS;uid=sa;pwd=BRYY@abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "Insert Into Products(RPID,PCode,BarCode,PName,PYName,UnitType,Alias,Model,FromPlace,ShortFromPlace,BaseUnit,UnitRate,Status,WareID,IsDrug,WTFromPlace,SRFH,MidUnitRate) Values(@RPID,@PCode,@BarCode,@PName,@PYName,@UnitType,@Alias,@Model,@FromPlace,@ShortFromPlace,@BaseUnit,@UnitRate,@Status,@WareID,@IsDrug,@WTFromPlace,@SRFH,@MidUnitRate); SELECT SCOPE_IDENTITY() AS id;";
                var result = Conn.ExecuteScalar<int>(query, product);
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int InsertStockDetailInit(int stockId, int wmsPbid, decimal quantity)
        {
            ConnectionFactory.ConnectionString = "Data Source=172.168.41.50;database=Bz_WMS;uid=sa;pwd=BRYY@abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "Insert Into StockDetail(StockID,PBID,Quantity,Quantity2,SubStatus) Values(@StockID,@PBID,@Quantity,0,0)";
                var result = Conn.Execute(query, new { StockID= stockId, PBID= wmsPbid, Quantity= quantity });
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int InsertStockInit()
        {
            ConnectionFactory.ConnectionString = "Data Source=172.168.41.50;database=Bz_WMS;uid=sa;pwd=BRYY@abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "INSERT INTO Stock(StoreID,FormTypeID,StockNumber,ContractNumber,WarehouseID,CreatorID,Status,Notes,SystemDate,SystemTime,ImportTime) VALUES (1,2,'10001','',1,1,9,'初始化入库',GETDATE(),GETDATE(),GETDATE()); SELECT SCOPE_IDENTITY() AS id;";
                var result = Conn.ExecuteScalar<int>(query, null);
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }

        public int ValProducts(int pId)
        {
            ConnectionFactory.ConnectionString = "Data Source=172.168.41.50;database=Bz_WMS;uid=sa;pwd=BRYY@abc123";
            using (var Conn = ConnectionFactory.Connection)
            {
                string query = "SELECT PID FROM dbo.Products WHERE RPID=@pId";
                var result = Conn.ExecuteScalar<int>(query, new { pId });
                Conn.Close();
                Conn.Dispose();
                return result;
            }
        }
    }
}

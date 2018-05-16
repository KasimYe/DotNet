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

* Filename: FlowImportDAL
* Namespace: Kasim.Core.SQLServerDAL.WebApi
* Classname: FlowImportDAL
* Created: 2018-05-07 21:50:25
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Factory.FlowImport;
using Kasim.Core.IDAL.WebApi;
using Kasim.Core.Model.WebApi.FlowImport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.SQLServerDAL.WebApi
{
    public class FlowImportDAL : IFlowImportDAL
    {
        public int AddClient(F_Client entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.Clients(ClientID,ClientCode,ClientName,PYName,BottomLevel,ClientType,bCustomer,bSupplier,Status) "
                    + "VALUES (@ClientID,@ClientCode,@ClientName,@PYName,@BottomLevel,@ClientType,@bCustomer,@bSupplier,@Status)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddClientPriceType(F_ClientPriceType entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.ClientPriceTypes(ClientID,PriceTypeID,SalesID) "
                    + "VALUES (@ClientID,@PriceTypeID,@SalesID)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddForm(F_Form entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.Form(OLDFID,StoreID,FormTypeID,FormNumber,RelatedFID,ContractNumber,ClientID,WarehouseID,WarehouseID2,CreatorID,SalesID,NoTaxSum,TaxSum,NoTaxPaidSum,TaxPaidSum,PayMethod,FirstPayDate,Status,Notes,ReviewerID,SystemDate,SystemTime) "
                    + "VALUES (@OLDFID,@StoreID,@FormTypeID,@FormNumber,@RelatedFID,@ContractNumber,@ClientID,@WarehouseID,@WarehouseID2,@CreatorID,@SalesID,@NoTaxSum,@TaxSum,@NoTaxPaidSum,@TaxPaidSum,@PayMethod,@FirstPayDate,@Status,@Notes,@ReviewerID,@SystemDate,@SystemTime);"
                    + "SELECT id=SCOPE_IDENTITY()";
                var reuslt = Conn.ExecuteScalar<int>(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddFormDetail(F_FormDetail entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.FormDetail(FID,PID,RelatedID,Batch,ExpiryDate,Quantity,Quantity2,Quantity3,GainLossReason,InPrice,WPrice,RPrice,NoTaxPrice,TaxPrice,NoTaxTotal,TaxTotal,PaidNoTaxTotal,PaidTaxTotal,ReviewerID,ReviewDate,WarehouseID3,Place,Checked,SubStatus,Note,MDPrice) "
                    + "VALUES (@FID,@PID,@RelatedID,@Batch,@ExpiryDate,@Quantity,@Quantity2,@Quantity3,@GainLossReason,@InPrice,@WPrice,@RPrice,@NoTaxPrice,@TaxPrice,@NoTaxTotal,@TaxTotal,@PaidNoTaxTotal,@PaidTaxTotal,@ReviewerID,@ReviewDate,@WarehouseID3,@Place,@Checked,@SubStatus,@Note,@MDPrice)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddGuser(F_Guser entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.g_Users(UserID,UserCode,LoginID,Password,UserName,UserGroupID,StoreID,DID,Telephone,ThePost,BirthDate,WorkTime,CallTime,PersonSex,PersonEdu,PersonProf,Qualification,Notes,Status,Super,StoreType) "
                    + "VALUES (@UserID,@UserCode,@LoginID,@Password,@UserName,@UserGroupID,@StoreID,@DID,@Telephone,@ThePost,@BirthDate,@WorkTime,@CallTime,@PersonSex,@PersonEdu,@PersonProf,@Qualification,@Notes,@Status,@Super,@StoreType)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddInv(F_Inv entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.Inv(PID,Quantity,Batch,LastUpdateDate) VALUES (@PID,@Quantity,@Batch,@LastUpdateDate)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddProduct(F_Product entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.Level5Products(PID,PCode5,PName,PYName,UnitType,Alias,Model,FromPlace,ShortFromPlace,PuRate,PTypeID,BaseUnit,UnitRate,Status,MUnitRate,LastClientId) "
                    + "VALUES (@PID,@PCode5,@PName,@PYName,@UnitType,@Alias,@Model,@FromPlace,@ShortFromPlace,@PuRate,@PTypeID,@BaseUnit,@UnitRate,@Status,@MUnitRate,@LastClientId)";
                var reuslt=Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddProductPrice(F_ProductPrice entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "INSERT INTO dbo.ProductPrices(PID,StoreID,PriceTypeID,ProductPrice,LastUpdate) "
                    + "VALUES (@PID,@StoreID,@PriceTypeID,@ProductPrice,@LastUpdate)";
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int SetFormStatus(int status)
        {
            return 0;
        }

        public int TruncateClientPriceTypes()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.ClientPriceTypes";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int TruncateClients()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.Clients";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int TruncateGusers()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.g_Users";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int TruncateInvs()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.Inv";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int TruncateProductPrices()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.ProductPrices";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int TruncateProducts()
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = "TRUNCATE TABLE dbo.Level5Products";
                var reuslt = Conn.Execute(query);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }
    }
}

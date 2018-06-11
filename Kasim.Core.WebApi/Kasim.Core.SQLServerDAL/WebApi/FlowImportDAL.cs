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
                var query = string.Format("SELECT COUNT(ClientID) FROM dbo.Clients WHERE ClientID={0}", entity.ClientID);
                var count = Conn.ExecuteScalar<int>(query);
                if (count == 0)
                {
                    query = "INSERT INTO dbo.Clients(ClientID,ClientCode,ClientName,PYName,BottomLevel,ClientType,bCustomer,bSupplier,Status) "
                   + "VALUES (@ClientID,@ClientCode,@ClientName,@PYName,@BottomLevel,@ClientType,@bCustomer,@bSupplier,@Status)";
                }
                else
                {
                    query = "UPDATE dbo.Clients SET ClientCode=@ClientCode,ClientName=@ClientName,PYName=@PYName,BottomLevel=@BottomLevel," +
                        "ClientType=@ClientType,bCustomer=@bCustomer,bSupplier=@bSupplier,Status=@Status WHERE ClientID=@ClientID";
                }
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
                var query = string.Format("SELECT COUNT(*) FROM dbo.ClientPriceTypes WHERE ClientID={0}", entity.ClientID);
                var count = Conn.ExecuteScalar<int>(query);
                if (count == 0)
                {
                    query = "INSERT INTO dbo.ClientPriceTypes(ClientID,PriceTypeID,SalesID) VALUES (@ClientID,@PriceTypeID,@SalesID)";
                }
                else
                {
                    query = "UPDATE dbo.ClientPriceTypes SET PriceTypeID=@PriceTypeID,SalesID=@SalesID WHERE ClientID=@ClientID";
                }
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
                var query = string.Format("SELECT COUNT(UserID) FROM dbo.g_Users WHERE UserID={0}", entity.UserID);
                var count = Conn.ExecuteScalar<int>(query);
                if (count==0)
                {
                    query = "INSERT INTO dbo.g_Users(UserID,UserCode,LoginID,Password,UserName,UserGroupID,StoreID,DID,Telephone,ThePost,BirthDate,WorkTime,CallTime,PersonSex,PersonEdu,PersonProf,Qualification,Notes,Status,Super,StoreType) "
                    + "VALUES (@UserID,@UserCode,@LoginID,@Password,@UserName,@UserGroupID,@StoreID,@DID,@Telephone,@ThePost,@BirthDate,@WorkTime,@CallTime,@PersonSex,@PersonEdu,@PersonProf,@Qualification,@Notes,@Status,@Super,@StoreType)";
                }
                else
                {
                    query = "UPDATE dbo.g_Users SET UserCode=@UserCode,LoginID=@LoginID,Password=@Password,UserName=@UserName,UserGroupID=@UserGroupID,StoreID=@StoreID,DID=@DID,Telephone=@Telephone,ThePost=@ThePost,BirthDate=@BirthDate," +
                        "WorkTime=@WorkTime,CallTime=@CallTime,PersonSex=@PersonSex,PersonEdu=@PersonEdu,PersonProf=@PersonProf,Qualification=@Qualification,Notes=@Notes,Status=@Status,Super=@Super,StoreType=@StoreType WHERE UserID=@UserID";
                }                
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
                var query = string.Format("SELECT COUNT(PID) FROM dbo.Level5Products WHERE PID={0}", entity.PID);
                var count = Conn.ExecuteScalar<int>(query);
                if (count == 0)
                {
                    query = "INSERT INTO dbo.Level5Products(PID,PCode5,PName,PYName,UnitType,Alias,Model,FromPlace,ShortFromPlace,PuRate,PTypeID,BaseUnit,UnitRate,Status,MUnitRate,LastClientId) "
                    + "VALUES (@PID,@PCode5,@PName,@PYName,@UnitType,@Alias,@Model,@FromPlace,@ShortFromPlace,@PuRate,@PTypeID,@BaseUnit,@UnitRate,@Status,@MUnitRate,@LastClientId)";
                }
                else
                {
                    query = "UPDATE dbo.Level5Products SET PCode5=@PCode5,PName=@PName,PYName=@PYName,UnitType=@UnitType,Alias=@Alias,Model=@Model,FromPlace=@FromPlace,ShortFromPlace=@ShortFromPlace," +
                        "PuRate=@PuRate,PTypeID=@PTypeID,BaseUnit=@BaseUnit,UnitRate=@UnitRate,Status=@Status,MUnitRate=@MUnitRate,LastClientId=@LastClientId WHERE PID=@PID";
                }
                var reuslt = Conn.Execute(query, entity);
                Conn.Close();
                Conn.Dispose();
                return reuslt;
            }
        }

        public int AddProductPrice(F_ProductPrice entity)
        {
            using (var Conn = ModelFactory.OpenConnection())
            {
                var query = string.Format("SELECT COUNT(*) FROM dbo.ProductPrices WHERE pID={0} AND StoreID={1} AND PriceTypeID={2}", entity.PID, entity.StoreID, entity.PriceTypeID);
                var count = Conn.ExecuteScalar<int>(query);
                if (count == 0)
                {
                    query = "INSERT INTO dbo.ProductPrices(PID,StoreID,PriceTypeID,ProductPrice,LastUpdate) "
                                        + "VALUES (@PID,@StoreID,@PriceTypeID,@ProductPrice,@LastUpdate)";
                }
                else
                {
                    query = "UPDATE dbo.ProductPrices SET ProductPrice=@ProductPrice WHERE pID=@PID AND StoreID=@StoreID AND PriceTypeID=@PriceTypeID";
                }
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

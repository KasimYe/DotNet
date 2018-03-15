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

* Filename: FileDAL
* Namespace: Kasim.Core.MySQLDAL.FileUploadWebApp
* Classname: FileDAL
* Created: 2018-03-13 14:01:06
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Common;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.FileUploadWebApp;
using Kasim.Core.Model.FileUploadWebApp;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Kasim.Core.MySQLDAL.FileUploadWebApp
{
    public class FileDAL : IFileDAL<FileModel>
    {
        public int Delete(FileModel entity)
        {
            throw new NotImplementedException();
        }

        public FileModel GetEntity(object id)
        {
            throw new NotImplementedException();
        }

        public int? GetFileId(File file, int fModelId)
        {
            using (var Conn = new MySqlConnection(ConnectionFactory.MySqlConnectionString))
            {
                var query = "SELECT `FId` FROM `filedb`.`file` WHERE `FMd5`=@Md5 AND `FModelId`=@FModelId;";
                Conn.Open();
                var result = Conn.ExecuteScalar(query, new
                {
                    file.Md5,
                    FModelId = fModelId
                });
                Conn.Close();
                Conn.Dispose();
                return (int?)result;
            }
        }

        public FileModel GetFileModel(FileModel fileMode)
        {
            using (var Conn = new MySqlConnection(ConnectionFactory.MySqlConnectionString))
            {
                var _table = fileMode.Table;
                var _keys = MySecurity.SEncryptString(JsonConvert.SerializeObject(fileMode.Keys), "yss.yh");
                var _vals = MySecurity.SEncryptString(JsonConvert.SerializeObject(fileMode.Vals), "yss.yh");
                var query = "SELECT `FModelId` FROM `filedb`.`filemodel` WHERE `CTypeId`=@TypeId AND `Source`=@Table AND `Keys`=@Keys AND `Values`=@Vals LIMIT 1;";
                Conn.Open();
                var result = Conn.ExecuteScalar<int>(query, new
                {
                    fileMode.TypeId,
                    Table = _table,
                    Keys = _keys,
                    Vals = _vals
                });
                query = "SELECT`FId` AS Id,`FTypeId`,`FModelId`,`FName` AS Name,`FUrl` AS Url,`FTime` AS Time,`FMd5` AS Md5,`FUser` FROM `filedb`.`file` WHERE `FModelId`=@FModelId";
                var list = Conn.Query<File>(query, new
                {
                    FModelId = result
                }).ToList();
                fileMode.Id = result;
                fileMode.FileList = list;
                Conn.Close();
                Conn.Dispose();
                return fileMode;
            }
        }

        public int? GetFileModelId(FileModel fileMode)
        {
            using (var Conn = new MySqlConnection(ConnectionFactory.MySqlConnectionString))
            {
                var _table = fileMode.Table;
                var _keys = MySecurity.SEncryptString(JsonConvert.SerializeObject(fileMode.Keys), "yss.yh");
                var _vals = MySecurity.SEncryptString(JsonConvert.SerializeObject(fileMode.Vals), "yss.yh");
                var query = "SELECT `FModelId` FROM `filedb`.`filemodel` WHERE `CTypeId`=@TypeId AND `Source`=@Table AND `Keys`=@Keys AND `Values`=@Vals LIMIT 1;";
                Conn.Open();
                var result = Conn.ExecuteScalar(query, new
                {
                    fileMode.TypeId,
                    Table = _table,
                    Keys = _keys,
                    Vals = _vals
                });
                Conn.Close();
                Conn.Dispose();
                return (int?)result;
            }
        }

        public List<FileModel> GetList()
        {
            throw new NotImplementedException();
        }

        public int Insert(FileModel fileMode, File file)
        {
            using (var Conn = new MySqlConnection(ConnectionFactory.MySqlConnectionString))
            {
                var query = "INSERT INTO `filedb`.`file` (`FTypeId`,`FModelId`,`FName`,`FUrl`,`FTime`,`FMd5`,`FUser`)VALUES(@FTypeId,@FModelId,@FName,@FUrl,@FTime,@FMd5,@FUser);SELECT @@IDENTITY;";
                Conn.Open();
                var result = Conn.ExecuteScalar(query, new
                {
                    FTypeId = 1,
                    FModelId = fileMode.Id,
                    FName = file.Name,
                    FUrl = file.Url,
                    FTime = file.Time,
                    FMd5 = file.Md5,
                    FUser = "YH"
                });
                Conn.Close();
                Conn.Dispose();
                return Convert.ToInt32(result);
            }
        }

        public int Insert(FileModel entity)
        {
            using (var Conn = new MySqlConnection(ConnectionFactory.MySqlConnectionString))
            {
                var _table = entity.Table;
                var _keys = MySecurity.SEncryptString(JsonConvert.SerializeObject(entity.Keys), "yss.yh");
                var _vals = MySecurity.SEncryptString(JsonConvert.SerializeObject(entity.Vals), "yss.yh");
                var param = new DynamicParameters();
                param.Add("@p_CTypeId", entity.TypeId, DbType.Int32);
                param.Add("@p_Source", _table, DbType.String);
                param.Add("@p_Keys", _keys, DbType.String);
                param.Add("@p_Values", _vals, DbType.String);
                param.Add("@p_FModelId", 0, DbType.Int32, ParameterDirection.Output);
                Conn.Open();
                var Tran = Conn.BeginTransaction();
                try
                {
                    Conn.Execute("InsertFileModel", param, Tran, commandType: CommandType.StoredProcedure);
                    Tran.Commit();
                    var result = param.Get<int>("@p_FModelId");
                    return result;
                }
                catch (Exception ex)
                {                    
                    Tran.Rollback();
                    return 0;
                }
                finally
                {
                    Conn.Close();
                    Conn.Dispose();
                }




                //var query = "INSERT INTO `filedb`.`filemodel` (`CTypeId`,`Source`,`Keys`,`Values`)VALUES(@TypeId,@Table,@Keys,@Vals);SELECT @@IDENTITY;";
                //Conn.Open();
                //var result = Conn.ExecuteScalar(query, new
                //{
                //    entity.TypeId,
                //    Table = _table,
                //    Keys = _keys,
                //    Vals = _vals
                //});
                //Conn.Close();
                //Conn.Dispose();
                //return Convert.ToInt32(result);
            }
        }

        public int Update(FileModel entity)
        {
            throw new NotImplementedException();
        }
    }
}

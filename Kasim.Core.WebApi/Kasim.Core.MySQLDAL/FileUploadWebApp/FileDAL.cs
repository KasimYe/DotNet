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
using Kasim.Core.Factory;
using Kasim.Core.IDAL.FileUploadWebApp;
using Kasim.Core.Model.FileUploadWebApp;
using MySql.Data.MySqlClient;
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

        public File GetFileEntity(File file)
        {
            using (var Conn = ConnectionFactory.MySqlConnection)
            {
                var query = "SELECT `FId`,`FTypeId`,`FModelId`,`FName`,`FUrl`,`FTime`,`FMd5`,`FUser` FROM `filedb`.`file` WHERE FMd5=@FMd5;";
                var result = Conn.Query<File>(query, file).SingleOrDefault();
                Conn.Close();
                Conn.Dispose();
                return result ?? file;
            }
        }

        public List<FileModel> GetList()
        {
            throw new NotImplementedException();
        }

        public File Insert(FileModel fileMode, File file)
        {
            using (var Conn = ConnectionFactory.MySqlConnection)
            {
                var query = "INSERT INTO `filedb`.`file` (`FTypeId`,`FModelId`,`FName`,`FUrl`,`FTime`,`FMd5`,`FUser`)VALUES(@FTypeId,@FModelId,@FName,@FUrl,@FTime,@FMd5,@FUser);SELECT @@IDENTITY;";
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
                file.Id =Convert.ToInt32(result);
                Conn.Close();
                Conn.Dispose();
                return file;
            }
        }

        public int Insert(FileModel entity)
        {
            using (var Conn = ConnectionFactory.MySqlConnection)
            {
                var query = "INSERT INTO `filedb`.`filemodel` (`CTypeId`,`Source`,`Keys`,`Values`)VALUES(@TypeId,@Table,@Keys,@Vals);SELECT @@IDENTITY;";
                var keys = "";
                entity.Keys.AsList().ForEach(x => keys += x);
                var vals = "";
                entity.Vals.AsList().ForEach(x => vals += x);
                var result = Conn.ExecuteScalar(query, new
                {
                    entity.TypeId,
                    entity.Table,
                    Keys = keys,
                    Vals = vals
                });
                Conn.Close();
                Conn.Dispose();
                return Convert.ToInt32(result);
            }
        }

        public int Update(FileModel entity)
        {
            throw new NotImplementedException();
        }
    }
}

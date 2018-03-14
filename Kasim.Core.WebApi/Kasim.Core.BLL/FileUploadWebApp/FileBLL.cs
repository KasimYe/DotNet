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

* Filename: FileBLL
* Namespace: Kasim.Core.BLL.FileUploadWebApp
* Classname: FileBLL
* Created: 2018-03-13 13:26:34
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Common;
using Kasim.Core.Factory;
using Kasim.Core.IBLL.FileUploadWebApp;
using Kasim.Core.IDAL.FileUploadWebApp;
using Kasim.Core.Model.FileUploadWebApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.FileUploadWebApp
{
    public class FileBLL : IFileBLL
    {
        IFileDAL<FileModel> dal = DALFactory<IFileDAL<FileModel>>.CreateDAL("Kasim.Core.MySQLDAL", "FileUploadWebApp.FileDAL");

        public FileBLL(ConnectionStringOptions connectionStrings)
        {
            ConnectionFactory.MySqlConnectionString = connectionStrings.DefaultConnection;
        }

        public FileModel AddFiles(FileModel fileMode)
        {
            fileMode.Id = dal.Insert(fileMode);
            if (fileMode.Id == 0) return null;
            for (int i = 0; i < fileMode.FileList.Count; i++)
            {
                var file = fileMode.FileList[i];
                var _fullPath = file.FullPath;
                file.Md5 = MD5.GetFileMd5(_fullPath);
                file.Id = dal.GetFileId(file, (int)fileMode.Id);
                if (file.Id == null)
                {
                    file.Id = dal.Insert(fileMode, file);
                }
                else
                {
                    FileOperate.FileDel(_fullPath);
                }
            }
            return fileMode;
        }

        public FileModel GetFiles(FileModel fileMode)
        {
            return dal.GetFileModel(fileMode);
        }
    }
}

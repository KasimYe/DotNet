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

* Filename: IFileDAL
* Namespace: Kasim.Core.IDAL.FileUploadWebApp
* Classname: IFileDAL
* Created: 2018-03-13 13:59:58
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.FileUploadWebApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.IDAL.FileUploadWebApp
{
    public interface IFileDAL<T> : IBaseEntityDAL<FileModel>
    {
        File GetFileEntity(File file, int fModelId);
        File Insert(FileModel fileMode, File file);
        int? GetFileModelId(FileModel fileMode);
    }
}

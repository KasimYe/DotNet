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

* Filename: FileModel
* Namespace: Kasim.Core.Model.FileUploadWebApp
* Classname: FileModel
* Created: 2018-03-13 10:19:26
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.FileUploadWebApp
{
    public class FileModel : BaseEntity
    {
        public int TypeId { get; set; }
        public string Message { get; set; }
        public string Table { get; set; }
        public string[] Keys { get; set; }
        public string[] Vals { get; set; }
        public List<File> FileList { get; set; }

    }
}

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

* Filename: UnitTest3
* Namespace: Kasim.Core.Test
* Classname: UnitTest3
* Created: 2018-03-13 10:47:10
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.BLL.WebApi;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.Model.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Kasim.Core.Model.FileUploadWebApp;

namespace Kasim.Core.Test
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void Test()
        {
            var fileModel = new FileModel
            {
                TypeId = 3,
                Table = "Files",
                Keys = new string[] { "key1", "key2" },
                Vals = new string[] { "val1", "val2" },
                Message = "总有一条蜿蜒的河，世上只有爸爸好"
            };
            var json = JsonConvert.SerializeObject(fileModel);
            var txt = Common.MySecurity.SEncryptString(json, "yss.yh"); ;
            Console.WriteLine(txt);
        }
    }
}

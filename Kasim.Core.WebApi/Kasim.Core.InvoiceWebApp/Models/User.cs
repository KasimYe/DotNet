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

* Filename: User
* Namespace: Kasim.Core.InvoiceWebApp.Models
* Classname: User
* Created: 2018-03-29 10:05:24
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kasim.Core.InvoiceWebApp.Models
{
    public class User
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    //for simple, I'm not using the database to store the user data, just using a static class to replace it.
    public static class TestUserStorage
    {
        public static List<User> UserList { get; set; } = new List<User>() {
        new User { UserName = "admin",Password = "52033"}
    };
    }
}

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

* Filename: ModelFactory
* Namespace: Kasim.Core.Factory.Weixin
* Classname: ModelFactory
* Created: 2018-05-04 11:12:09
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Factory.Weixin
{
    public class ModelFactory
    {
        public static SenparcWeixinSetting SenparcWeixinSetting;
        public static Model.Weixin.ConnectionStringOptions ConnectionStringOptions;

        #region Connection对象初始化
        public static System.Data.SqlClient.SqlConnection OpenConnection(string cs)
        {
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cs);
            connection.Open();
            return connection;
        }

        public static MySql.Data.MySqlClient.MySqlConnection OpenmMySqlConnection(string cs)
        {
            MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(cs);
            connection.Open();
            return connection;
        }
        #endregion
    }
}

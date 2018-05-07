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

* Filename: UserActionDAL
* Namespace: Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
* Classname: UserActionDAL
* Created: 2018-05-04 10:57:22
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Dapper;
using Kasim.Core.Factory.Weixin;
using Kasim.Core.IDAL.WeiXin.MP.WebApp;
using Kasim.Core.Model.Weixin.MP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
{
    public class UserActionDAL : IUserActionDAL
    {
        public List<UserAction> GetListByKeyWord(string keyWord)
        {
            using (IDbConnection connection = ModelFactory.OpenConnection(ModelFactory.ConnectionStringOptions.DefaultConnection))
            {
                string strSql = string.Format("SELECT a.AccessId,a.WxUserId,a.KeyId,u.OpenId,u.NickName,u.Sex,u.City,u.Province,u.Country,t.KeyWord,u.AddTime,a.ApplyTel,a.ApplyNote,a.ValidTime,a.IsUsed "
                + "FROM dbo.WxUser u,dbo.TextRequestAccess a,dbo.TextRequest t WHERE u.WxUserId=a.WxUserId AND t.KeyId=a.KeyId AND a.IsUsed = 1 AND t.KeyWord='{0}'", keyWord);
                var result = connection.Query<UserAction>(strSql).ToList();
                connection.Close();
                connection.Dispose();
                return result;
            }
        }

        public int VerifyUserAction(UserAction userAction)
        {
            using (SqlConnection conn = new SqlConnection(ModelFactory.ConnectionStringOptions.DefaultConnection))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "SetUserAction";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@KeyWord", SqlDbType.VarChar, 20).Value = userAction.KeyWord;
                    comm.Parameters.Add("@WxUserGroupId", SqlDbType.Int).Value = 0;
                    comm.Parameters.Add("@OpenId", SqlDbType.VarChar, 100).Value = userAction.OpenId;
                    comm.Parameters.Add("@NickName", SqlDbType.VarChar, 100).Value = userAction.NickName;
                    comm.Parameters.Add("@Sex", SqlDbType.VarChar, 5).Value = userAction.Sex;
                    comm.Parameters.Add("@Language", SqlDbType.VarChar, 50).Value = userAction.Language;
                    comm.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = userAction.City;
                    comm.Parameters.Add("@Province", SqlDbType.VarChar, 50).Value = userAction.Province;
                    comm.Parameters.Add("@Country", SqlDbType.VarChar, 50).Value = userAction.Country;
                    comm.Parameters.Add("@SubscribeTime", SqlDbType.DateTime).Value = userAction.SubscribeTime;
                    comm.Parameters.Add("@ApplyTel", SqlDbType.VarChar, 20).Value = "";
                    comm.Parameters.Add("@ApplyNote", SqlDbType.VarChar, 200).Value = "";
                    comm.Parameters.Add("@ret", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    comm.ExecuteNonQuery();
                    string ret = comm.Parameters["@ret"].Value.ToString();

                    comm.CommandText = "VerifyUserAction";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@OpenId", SqlDbType.VarChar, 100).Value = userAction.OpenId;
                    comm.Parameters.Add("@KeyWord", SqlDbType.VarChar, 20).Value = userAction.KeyWord;
                    comm.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    comm.ExecuteNonQuery();
                    int reValue = int.Parse(comm.Parameters["RETURN_VALUE"].Value.ToString());
                    conn.Close();
                    comm.Dispose();
                    conn.Dispose();
                    return reValue;
                }
            }
        }
    }
}

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

* Filename: NewsInfoDAL
* Namespace: Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
* Classname: NewsInfoDAL
* Created: 2018-05-04 10:55:53
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
using System.Text;

namespace Kasim.Core.SQLServerDAL.WeiXin.MP.WebApp
{
    public class NewsInfoDAL : INewsInfoDAL
    {
        public bool CheckSendNews(NewsInfo newsInfo)
        {
            using (IDbConnection connection = ModelFactory.OpenConnection(ModelFactory.ConnectionStringOptions.DefaultConnection))
            {
                string strSql = string.Format("SELECT ReSend FROM dbo.SendNewsInfo WHERE NewsType='{0}' AND PrimaryKey='{1}'",
                newsInfo.NewsType, newsInfo.PrimaryKey);
                if (!string.IsNullOrEmpty(newsInfo.OpenId))
                {
                    strSql += string.Format(" AND OpenId='{0}'", newsInfo.OpenId);
                }
                if (newsInfo.NewsDate != null)
                {
                    strSql += string.Format(" AND NewsDate='{0}'", ((DateTime)newsInfo.NewsDate).ToShortDateString());
                }
                var result = connection.ExecuteScalar(strSql);
                connection.Close();
                connection.Dispose();
                return result == null ? true : Convert.ToBoolean(result);
            }
        }

        public int SetSendNewsInfo(NewsInfo info)
        {
            using (SqlConnection conn = new SqlConnection(ModelFactory.ConnectionStringOptions.DefaultConnection))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "SetSendNewsInfo";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@NewsType", SqlDbType.NVarChar, 50).Value = info.NewsType;
                    comm.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 20).Value = info.PrimaryKey;
                    if (info.NewsDate != null)
                    {
                        comm.Parameters.Add("@NewsDate", SqlDbType.Date).Value = info.NewsDate;
                    }
                    if (!string.IsNullOrEmpty(info.OpenId))
                    {
                        comm.Parameters.Add("@OpenId", SqlDbType.VarChar, 100).Value = info.OpenId;
                    }
                    comm.Parameters.Add("@ReSend", SqlDbType.Bit).Value = info.ReSend;
                    var result = comm.ExecuteNonQuery();
                    conn.Close();
                    comm.Dispose();
                    conn.Dispose();
                    return result;
                }
            }
        }
    }
}

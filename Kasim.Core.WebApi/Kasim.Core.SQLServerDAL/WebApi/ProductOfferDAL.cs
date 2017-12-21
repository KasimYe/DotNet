/**
 *                             _ooOoo_
 *                            o8888888o
 *                            88" . "88
 *                            (| -_- |)
 *                            O\  =  /O
 *                         ____/`---'\____
 *                       .'  \\|     |//  `.
 *                      /  \\|||  :  |||//  \
 *                     /  _||||| -:- |||||-  \
 *                     |   | \\\  -  /// |   |
 *                     | \_|  ''\---/''  |   |
 *                     \  .-\__  `-`  ___/-. /
 *                   ___`. .'  /--.--\  `. . __
 *                ."" '<  `.___\_<|>_/___.'  >'"".
 *               | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *               \  \ `-.   \_ __\ /__ _/   .-` /  /
 *          ======`-.____`-.___\_____/___.-`____.-'======
 *                             `=---='
 *          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
 *                     佛祖保佑        永无BUG
 *            佛曰:
 *                   写字楼里写字间，写字间里程序员；
 *                   程序人员写程序，又拿程序换酒钱。
 *                   酒醒只在网上坐，酒醉还来网下眠；
 *                   酒醉酒醒日复日，网上网下年复年。
 *                   但愿老死电脑间，不愿鞠躬老板前；
 *                   奔驰宝马贵者趣，公交自行程序员。
 *                   别人笑我忒疯癫，我笑自己命太贱；
 *                   不见满街漂亮妹，哪个归得程序员？
*/
/*----------------------------------------------------------------
** Copyright (C) 2017 
**
** file：ProductOfferDAL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2017-12-20 15:24:18
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Dapper;
using Kasim.Core.Factory;
using Kasim.Core.IDAL.WebApi;
using Kasim.Core.Model.WebApi.ProductOffer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kasim.Core.SQLServerDAL.WebApi
{
    public class ProductOfferDAL : IProductOfferDAL
    {
        private IDbConnection _conn;

        public IDbConnection Conn
        {
            get
            {
                var connString = ConnectionFactory.ConnectionStrings.B2bConnection;
                return _conn = ConnectionFactory.CreateConnection(connString);
            }
        }

        public List<ProductsWebOffer> GetListByProductId(int productId)
        {
            using (Conn)
            {
                string query = "SELECT o.StartDate,o.EndDate,o.OfferNotes,o.OfferRemain,o.OfferPrice,t.TypeName,g.GroupNotes "
                    + "FROM dbo.Products_WebOffer o LEFT JOIN dbo.OfferTypes t ON o.OfferTypeID=t.OfferTypeID LEFT JOIN dbo.OfferGroups g ON o.OfferGroupID=g.OfferGroupID "
                    + "WHERE Enable=1 AND ProductID=@ProductID";
                var result = Conn.Query<ProductsWebOffer, OfferTypes, OfferGroups, ProductsWebOffer>(query,
                    (productsWebOffer, offerTypes, offerGroups) =>
                    {
                        productsWebOffer.OfferType = offerTypes;
                        productsWebOffer.OfferGroup = offerGroups;
                        return productsWebOffer;
                    }
                    , new { ProductID = productId }, splitOn: "TypeName,GroupNotes").AsList();
                return result;
            }
        }

        public int GetProductIDByErpPID(int pID)
        {
            using (Conn)
            {
                string query = "SELECT ProductID FROM dbo.Products WHERE BrPID=@BrPID";
                var result = (int)Conn.ExecuteScalar(query, new { BrPID = pID });
                return result;
            }
        }
    }
}

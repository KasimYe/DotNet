/*********************************************************************
 **                             _ooOoo_                             **
 **                            o8888888o                            **
 **                            88" . "88                            **
 **                            (| -_- |)                            **
 **                            O\  =  /O                            **
 **                         ____/`---'\____                         **
 **                       .'  \\|     |//  `.                       **
 **                      /  \\|||  :  |||//  \                      **
 **                     /  _||||| -:- |||||-  \                     **
 **                     |   | \\\  -  /// |   |                     **
 **                     | \_|  ''\---/''  |   |                     **
 **                     \  .-\__  `-`  ___/-. /                     **
 **                   ___`. .'  /--.--\  `. . __                    **
 **                ."" '<  `.___\_<|>_/___.'  >'"".                 **
 **               | | :  `- \`.;`\ _ /`;.`/ - ` : | |               **
 **               \  \ `-.   \_ __\ /__ _/   .-` /  /               **
 **          ======`-.____`-.___\_____/___.-`____.-'======          **
 **                             `=---='                             **
 **          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^          **
 **                     佛祖保佑        永无BUG                     **
 **            佛曰:                                                **
 **                   写字楼里写字间，写字间里程序员；              **
 **                   程序人员写程序，又拿程序换酒钱。              **
 **                   酒醒只在网上坐，酒醉还来网下眠；              **
 **                   酒醉酒醒日复日，网上网下年复年。              **
 **                   但愿老死电脑间，不愿鞠躬老板前；              **
 **                   奔驰宝马贵者趣，公交自行程序员。              **
 **                   别人笑我忒疯癫，我笑自己命太贱；              **
 **                   不见满街漂亮妹，哪个归得程序员？              **
 *********************************************************************/
/*=====================================================================
* Copyright (c) 2018 All Rights Reserved.
* CLRVer.:4.0.30319.42000
* machinenameDESKTOP-U288O1H
* namespace:Kasim.Core.MySQLDAL.HtmlAgilityPack
* filename:CxfccsDAL
* guid:6f1fb7a9-bd0f-4d83-9643-a7f5ecc9f8d4
* auth:lip86
* date:2018-02-23 20:51:44
* desc:
*
*=====================================================================*/
using Dapper;
using Kasim.Core.IDAL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kasim.Core.MySQLDAL.HtmlAgilityPack
{
    public class CxfccsDAL : ICxfccsDAL
    {
        public void AddList(List<Rent> list)
        {
            
            using (IDbConnection Conn = new MySqlConnection("Data Source=localhost;port=3306;Initial Catalog=cxfccs;user id=root;password=abc123;Charset=utf8"))
            {
                Conn.Open();
                string query = "INSERT INTO `cxfccs`.`rents` (`rentId`,`fromType`,`region`,`rentName`,`houseType`,`houseHold`,`floor`,`floorCount`,`renovation`,"
                    + "`area`,`garage`,`rentMethod`,`houseOrientation`,`payType`,`houseYear`,`rentPrice`,`facilities`,`remark`,`imgCount`,`url`,`updateTime`) "
                    + "VALUES(@rentId,@fromType,@region,@rentName,@houseType,@houseHold,@floor,@floorCount,@renovation,@area,@garage,@rentMethod,"
                    + "@houseOrientation,@payType,@houseYear,@rentPrice,@facilities,@remark,@imgCount,@url,@updateTime) ;";
                foreach (var entity in list)
                {
                    Conn.Execute(query, entity);
                }                
                Conn.Close();
                Conn.Dispose();
            }
        }
    }
}

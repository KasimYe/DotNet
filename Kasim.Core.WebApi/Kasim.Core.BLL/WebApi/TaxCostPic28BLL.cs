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
** file：TaxCostPic28BLL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2017-12-05 13:20:10
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Kasim.Core.IBLL.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Model.WebApi;
using Kasim.Core.IDAL.WebApi;
using Kasim.Core.SQLServerDAL.WebApi;
using Kasim.Core.Factory;

namespace Kasim.Core.BLL.WebApi
{
    public class TaxCostPic28BLL : ITaxCostPic28BLL
    {
        ITaxCostPic28DAL taxCostPic28DAL = DALFactory.CreateTaxCostPic28DAL();

        public TaxCostPic28BLL(ConnectionStringOptions connectionStrings)
        {
            ConnectionFactory.ConnectionString = connectionStrings.DevConnection;
        }

        public TaxCostPic28 GetImgByCostID(TaxCostPic28 taxCostPic)
        {
            return taxCostPic28DAL.GetEntity(taxCostPic.CostID);
        }

        public string GetImgUrlByFileName(string fileName)
        {
            try
            {
                string strInvoiceID = fileName.Substring(0, fileName.IndexOf('_'));
                string strInvoiceCode = fileName.Substring(strInvoiceID.Length + 1, fileName.IndexOf('.') - fileName.IndexOf('_') - 1);
                return taxCostPic28DAL.GetEntityByTax(strInvoiceCode, strInvoiceID).PicUrl;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public int SetImgMd5(TaxCostPic28 taxCostPic)
        {
            try
            {
                return taxCostPic28DAL.UpdateMd5(taxCostPic);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

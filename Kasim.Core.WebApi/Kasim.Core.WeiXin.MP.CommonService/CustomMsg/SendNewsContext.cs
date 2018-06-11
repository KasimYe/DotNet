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

* Filename: SendNewsContext
* Namespace: Kasim.Core.WeiXin.MP.CommonService.CustomMsg
* Classname: SendNewsContext
* Created: 2018-05-04 10:15:37
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.BLL.WeiXin.MP.WebApp;
using Kasim.Core.Factory.Weixin;
using Kasim.Core.IBLL.WeiXin.MP.WebApp;
using Kasim.Core.Model.Weixin.MP;
using Kasim.Core.WeiXin.MP.CommonService.TemplateMessage;
using Microsoft.Extensions.Options;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kasim.Core.WeiXin.MP.CommonService.CustomMsg
{
    public class SendNewsContext
    {
        public static void SendNewsQHProducts()
        {

            IQhProductBLL qhProductBLL = new QhProductBLL();
            List<QhProduct> qhProducts = qhProductBLL.GetQhProducts();
            IUserActionBLL userActionBLL = new UserActionBLL();
            List<UserAction> userActions = userActionBLL.GetUserActionListByKeyWord("缺货");
            INewsInfoBLL newsInfoBLL = new NewsInfoBLL();
            foreach (var qp in qhProducts)
            {
                foreach (var action in userActions)
                {
                    try
                    {
                        SerializerHelper serializerHelper = new SerializerHelper();
                        var salerList = serializerHelper.GetObject<List<Saler>>(action.ApplyNote);
                        if (salerList.Where(x => x.UserName == qp.SalesName).Count() > 0)
                        {
                            NewsInfo newsInfo = new NewsInfo()
                            {
                                NewsType = action.KeyWord,
                                PrimaryKey = qp.Id.ToString(),
                                NewsDate = DateTime.Now,
                                OpenId = action.OpenId,
                                ReSend = false
                            };
                            if (newsInfoBLL.CheckSendNews(newsInfo))
                            {
                                var qhNews = string.Format("业务员: [{0}],\r\n您的客户:  {1} \r\n在[{2}]订货,\r\n产品: {3}/{4}/{5},\r\n缺货数量: {6},\r\n于 {7} 已到货,\r\n请及时通知客户.",
                                         qp.SalesName, qp.ClientName, qp.SystemDate.ToString("yyyy-MM-dd HH:mm"), qp.PName, qp.Model, qp.FromPlace, qp.OrderQty.ToString("0.###"), qp.InDate.ToString("yyyy-MM-dd HH:mm"));
                                WeixinTemplate_QhProduct weixinTemplate_QhProduct = new WeixinTemplate_QhProduct(
                                    string.Format("业务员: {0},\r\n您的客户:  {1} \r\n在{2}订货", qp.SalesName, qp.ClientName, qp.SystemDate.ToString("yyyy-MM-dd HH:mm")),
                                    string.Format("{0}/{1}/{2}", qp.PName, qp.Model, qp.FromPlace),
                                    qp.InQty.ToString("0.###"), qp.InDate.ToString("yyyy-MM-dd HH:mm"), string.Format("缺货数量：{0},请及时通知客户", qp.OrderQty.ToString("0.###"))
                                    );
                                Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(ModelFactory.SenparcWeixinSetting.WeixinAppId, action.OpenId, weixinTemplate_QhProduct);
                                newsInfoBLL.SetSendNewsInfo(newsInfo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WeixinTrace.SendCustomLog("SendNewsQHProducts定时任务", ex.Message + "|" + action.ApplyNote);
                        continue;
                    }
                }
            }
        }

        public static void SendNewsTaxBills()
        {
            ITaxBillBLL taxBillBLL = new TaxBillBLL();
            List<TaxBill> taxBills = taxBillBLL.GetTaxBillList();
            IUserActionBLL userActionBLL = new UserActionBLL();
            List<UserAction> userActions = userActionBLL.GetUserActionListByKeyWord("发票");
            INewsInfoBLL newsInfoBLL = new NewsInfoBLL();
            foreach (var tb in taxBills)
            {
                foreach (var action in userActions)
                {
                    try
                    {
                        SerializerHelper serializerHelper = new SerializerHelper();
                        var clientList = serializerHelper.GetObject<List<TaxUser>>(action.ApplyNote);
                        if (clientList.Where(x => x.Name == tb.ClientName).Count() > 0)
                        {

                            NewsInfo newsInfo = new NewsInfo()
                            {
                                NewsType = "发票",
                                PrimaryKey = tb.FormNumber.ToString(),
                                NewsDate = DateTime.Now,
                                OpenId = action.OpenId,
                                ReSend = false
                            };

                            if (newsInfoBLL.CheckSendNews(newsInfo))
                            {

                                string url = null;
                                string fpzt = "此发票为纸质发票";
                                if (tb.Djzt == 11)
                                {
                                    url = string.Format("http://115.231.58.130:9006/Invoice/Details/{0}", tb.FormNumber);
                                    fpzt = "此发票为电子发票,点击详情可查看发票图片";
                                }
                                WeixinTemplate_TaxBill weixinTemplate_TaxBill = weixinTemplate_TaxBill = new WeixinTemplate_TaxBill(
                                    string.Format("您好,您有发票开具成功.\r\n流水号:{0}", tb.FormNumber), tb.ClientName,
                                   string.Format("{0}元\r\n发票号码：{1}", tb.TaxTotal.ToString("0.00##"), tb.TaxFphm), tb.TaxFpdm, tb.TaxFprq.ToString("yyyy-MM-dd"),
                                   string.Format("随货联单号:{0}\r\n{1}", tb.Note, fpzt), url);
                                Senparc.Weixin.MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(ModelFactory.SenparcWeixinSetting.WeixinAppId, action.OpenId, weixinTemplate_TaxBill);
                                newsInfoBLL.SetSendNewsInfo(newsInfo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WeixinTrace.SendCustomLog("SendNewsTaxBills定时任务", ex.Message);
                        continue;
                    }
                }
            }
        }

        public static void SendNewsHYOrder()
        {

        }

        public static void SendNewsSaleBills()
        {

        }
    }
}

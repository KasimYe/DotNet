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

* Filename: ResponseContext
* Namespace: Kasim.Core.WeiXin.MP.CommonService.CustomMsg
* Classname: ResponseContext
* Created: 2018-05-04 22:21:08
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.BLL.WeiXin.MP.WebApp;
using Kasim.Core.Factory.Weixin;
using Kasim.Core.IBLL.WeiXin.MP.WebApp;
using Kasim.Core.Model.Weixin.MP;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Kasim.Core.WeiXin.MP.CommonService.CustomMsg
{
    public class ResponseContext
    {
        private static readonly string actionUrl = "http://kasimye.nat123.net";

        internal static Senparc.Weixin.MP.Entities.IResponseMessageBase GetOpenResponseMessage(RequestMessageText requestMessage)
        {
            var keyWord = requestMessage.Content;
            var openId = requestMessage.FromUserName;//获取OpenId
            var appId = ModelFactory.SenparcWeixinSetting.WeixinAppId;
            return GetResponseMessage(appId, openId, keyWord, requestMessage, null);
        }

        internal static Senparc.Weixin.MP.Entities.IResponseMessageBase GetOpenResponseMessage(RequestMessageEvent_Click requestMessage)
        {
            var keyWord = requestMessage.EventKey;
            var openId = requestMessage.FromUserName;//获取OpenId
            var appId = ModelFactory.SenparcWeixinSetting.WeixinAppId;
            return GetResponseMessage(appId, openId, keyWord, null, requestMessage);
        }

        private static Senparc.Weixin.MP.Entities.IResponseMessageBase GetResponseMessage(string appId, string openId, string keyWord,
            RequestMessageText requestMessage, RequestMessageEvent_Click requestMessage_Event)
        {
            try
            {
                var userInfo = UserApi.Info(appId, openId, Language.zh_CN);
                IUserActionBLL userActionBLL = new UserActionBLL();
                var userAction = new UserAction
                {
                    KeyWord = keyWord,
                    OpenId = openId,
                    NickName = userInfo.nickname,
                    Sex = ((Sex)userInfo.sex).ToString(),
                    Language = userInfo.language,
                    City = userInfo.city,
                    Province = userInfo.province,
                    Country = userInfo.country,
                    SubscribeTime = DateTimeHelper.GetDateTimeFromXml(userInfo.subscribe_time)
                };
                int reValue = userActionBLL.VerifyUserAction(userAction);
                if (reValue == 1)
                {
                    var faultTolerantResponseMessage = requestMessage_Event == null ? requestMessage.CreateResponseMessage<ResponseMessageText>() : requestMessage_Event.CreateResponseMessage<ResponseMessageText>();
                    faultTolerantResponseMessage.Content = string.Format("输入的命令为无效请求");
                    return faultTolerantResponseMessage;
                }
                else if (reValue == 2)
                {
                    var openResponseMessage = requestMessage_Event == null ? requestMessage.CreateResponseMessage<ResponseMessageNews>() : requestMessage_Event.CreateResponseMessage<ResponseMessageNews>();
                    openResponseMessage.Articles.Add(NoActionArticle(openId, keyWord, userInfo.nickname, appId));
                    return openResponseMessage;
                }
                else if (reValue == 3)
                {
                    var faultTolerantResponseMessage = requestMessage_Event == null ? requestMessage.CreateResponseMessage<ResponseMessageText>() : requestMessage_Event.CreateResponseMessage<ResponseMessageText>();
                    faultTolerantResponseMessage.Content = string.Format("此功能权限尚未审核完毕\r\n请耐心等待");
                    return faultTolerantResponseMessage;
                }
                else
                {
                    var openResponseMessage = requestMessage_Event == null ? requestMessage.CreateResponseMessage<ResponseMessageNews>() : requestMessage_Event.CreateResponseMessage<ResponseMessageNews>();
                    List<Article> list = ActionArticles(openId, keyWord);
                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0; i < (list.Count > 5 ? 5 : list.Count); i++)
                        {
                            openResponseMessage.Articles.Add(list[i]);
                        }
                    }
                    else
                    {
                        var article = new Article()
                        {
                            Title = "麦斯康莱公众平台信息系统",
                            Description = string.Format("{0},您好!\r\n感谢您的使用麦斯康莱公众平台信息系统\r\n如有最新业务消息,将第一时间推送给您.", userInfo.nickname),
                            //Url = actionUrl
                        };
                        openResponseMessage.Articles.Add(article);
                    }
                    return openResponseMessage;
                }
            }
            catch (Exception ex)
            {
                WeixinTrace.SendCustomLog("GetResponseMessage文字按钮请求", ex.Message + "|" + openId + "|" + keyWord);
                return null;
            }
        }

        private static List<Article> ActionArticles(string openId, string keyWord)
        {
            List<Article> list = new List<Article>();
            Article article = null;
            string strDes;
            switch (keyWord)
            {
                case "订单":
                    //context = new ArticleContext() { configuration = _configuration };
                    //strDes = context.GetOrderBills(openId);
                    //article = new Article()
                    //{
                    //    Title = "麦斯康莱商城订单查询",
                    //    Description = strDes,
                    //    //Url = actionUrl
                    //};
                    //list.Add(article);
                    break;
                case "出库":
                    //context = new ArticleContext() { configuration = _configuration };
                    //context.SendNewsSaleBills(_appId, openId);
                    break;
                case "发票":
                    SendNewsContext.SendNewsTaxBills();
                    break;
                case "集单":
                    //context = new ArticleContext() { configuration = _configuration };
                    //strDes = context.GetHYOrders();
                    //article = new Article()
                    //{
                    //    Title = "麦斯康莱在途集单查询",
                    //    Description = strDes,
                    //    //Url = actionUrl
                    //};
                    //list.Add(article);
                    break;
                case "关账":
                    //context = new ArticleContext() { configuration = _configuration };
                    //string[] strDess = {
                    //    context.SetSystemDateMonth(2, 0),
                    //    context.SetSystemDateMonth(8, 0),
                    //    context.SetSystemDateMonth(15, 0),
                    //    context.SetSystemDateMonth(1, 1)
                    //};
                    //article = new Article()
                    //{
                    //    Title = "麦斯康莱月结关账查询",
                    //    Description = string.Format("在库商品库\r\n{0}\r\n\r\n中药经营部\r\n{1}\r\n\r\n器化经营部\r\n{2}\r\n\r\n在途商品库\r\n{3}", strDess[3], strDess[0], strDess[1], strDess[2]),
                    //    Url = actionUrl
                    //};
                    //list.Add(article);
                    break;
                case "缺货":
                    SendNewsContext.SendNewsQHProducts();
                    break;
                default:
                    return null;
            }
            return list;
        }

        private static Article NoActionArticle(string openId, string keyWord, string nickname, string appId)
        {
            Article article = null;
            switch (keyWord)
            {
                case "订单":
                    article = new Article()
                    {
                        Title = "麦斯康莱商城订单查询",
                        Description = "点击进入Open授权页面。\r\n\r\n您的微信号尚未获得此功能的授权。\r\n\r\n授权之后，您将可以查询到您的商城订单信息。"
                    };
                    break;
                case "出库":
                    article = new Article()
                    {
                        Title = "麦斯康莱销售出库状态查询",
                        Description = "点击进入Open授权页面。\r\n\r\n您的微信号尚未获得此功能的授权。\r\n\r\n授权之后，您将可以查询到您的销售单出库的状态信息。"
                    };
                    break;
                case "发票":
                    article = new Article()
                    {
                        Title = "麦斯康莱增值税发票查询",
                        Description = "点击进入Open授权页面。\r\n\r\n您的微信号尚未获得此功能的授权。\r\n\r\n授权之后，您将可以查询到您的增值税发票信息。"
                    };
                    break;
                case "集单":
                    article = new Article()
                    {
                        Title = "麦斯康莱在途集单查询",
                        Description = "点击进入Open授权页面。\r\n\r\n您的微信号尚未获得此功能的授权。\r\n\r\n授权之后，您将可以查询到在途集单信息。"
                    };
                    break;
                case "关账":
                    article = new Article()
                    {
                        Title = "麦斯康莱月结关账查询",
                        Description = "点击进入Open授权页面。\r\n\r\n您的微信号尚未获得此功能的授权。\r\n\r\n授权之后，您将可以查询到ERP系统关账信息。"
                    };
                    break;
                default:
                    return null;
            }
            article.Url = string.Format("{0}/Action?openid={1}&nickname={2}&requestname={3}&appid={4}", actionUrl, openId, nickname, keyWord, appId);
            return article;
        }
    }
}

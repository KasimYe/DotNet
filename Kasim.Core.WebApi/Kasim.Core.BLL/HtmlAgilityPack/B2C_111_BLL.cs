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
* Copyright (c) 2017 All Rights Reserved.
* CLRVer.:4.0.30319.42000
* machinenameDESKTOP-U288O1H
* namespace:Kasim.Core.BLL.HtmlAgilityPack
* filename:B2C_111_BLL
* guid:167b36c3-9eef-4444-91c1-aed143e65d86
* auth:lip86
* date:2017-12-14 19:40:21
* desc:
*
*=====================================================================*/
using HtmlAgilityPack;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class B2C_111_BLL : IB2C_111_BLL
    {
        public int GetPageCountByFirstPage(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[3]/div[2]/div[2]/div[2]/div[2]/div[2]/a[6]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            return int.Parse(reqObj.InnerText);
        }

        public List<B2C_111> ParsePageByProductType(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[3]/div[2]/div[2]/div[2]/div[2]/div[1]/ul[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            //var sb = new StringBuilder();
            var list = new List<B2C_111>();
            if (reqObj != null)
            {
                var parentNodeList = reqObj.SelectNodes(@"li");
                //商品列表
                foreach (var li in parentNodeList)
                {
                    //商品促销组合列表
                    foreach (var li_div in li.SelectNodes(@"div"))
                    {
                        var pname = li_div.SelectNodes(@"p")[1].InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                        var price = li_div.SelectNodes(@"p")[0].InnerText.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                        var sp = price.Split("&#165;");
                        string total;
                        if (sp.Length==2)
                        {
                            total = sp[0];
                            price = sp[1].Replace("/件", "");
                        }
                        else
                        {
                            total = price;
                        }
                        list.Add(new B2C_111() {
                            PName = pname,
                            Price=decimal.Parse(price),
                            Total=decimal.Parse(total)
                        });
                        //sb.Append(string.Format("name:{0} price:{1}\r\n", pname, price));

                    }
                }
            }
            return list;
        }
    }
}

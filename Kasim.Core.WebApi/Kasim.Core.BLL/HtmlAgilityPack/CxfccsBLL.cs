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
* namespace:Kasim.Core.BLL.HtmlAgilityPack
* filename:CxfccsBLL
* guid:728eaac2-c75a-43b2-82ef-7e5b5584b685
* auth:lip86
* date:2018-02-23 19:33:52
* desc:
*
*=====================================================================*/
using HtmlAgilityPack;
using Kasim.Core.Factory;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.IDAL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class CxfccsBLL : ICxfccsBLL
    {
        IWebClientBLL webClientBLL;
        ICxfccsDAL dal = DALFactory<ICxfccsDAL>.CreateDAL("Kasim.Core.MySQLDAL", "HtmlAgilityPack.CxfccsDAL");
        public CxfccsBLL()
        {
            webClientBLL = new WebClientBLL();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void DwonloadRents()
        {
            var url = "http://www.cxfccs.com/rent/";
            string html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("GB2312"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[3]/div[1]/div[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            if (reqObj != null)
            {
                var ulList = reqObj.SelectNodes(@"ul");
                var pageCount = ulList[ulList.Count - 1].SelectNodes("div")[0].SelectNodes("span")[0].SelectNodes("font")[1].InnerText;//.SelectSingleNode(@"/div[1]/span[1]/font[2]");
                Console.WriteLine(string.Format("总页数[{0}]", pageCount));
                var list = new List<Rent>();
                for (int i = 1; i <= int.Parse(pageCount); i++)
                {
                    url = "http://www.cxfccs.com/rent/page" + i.ToString() + ".html";
                    list.AddRange(GetRentList(url));
                    Console.WriteLine(string.Format("第[{0}]页所有信息内容获取成功", i));
                }
                dal.AddList(list);
                Console.WriteLine("下载完毕");
            }
        }

        public Rent GetRent(Rent rent)
        {
            string html = webClientBLL.GetWebClient(rent.Url, Encoding.GetEncoding("GB2312"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[3]/div[1]/div[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            if (reqObj != null)
            {
                var ulList = reqObj.SelectNodes(@"ul");
                if (ulList!=null && ulList.Count>0)
                {
                    for (int i = 1; i < ulList.Count; i++)
                    {
                        var li = ulList[i].SelectNodes("li");
                        for (int j = 0; j < li.Count; j++)
                        {
                            switch (li[j].InnerText.Trim())
                            {
                                case "更新时间：":
                                    rent.UpdateTime =DateTime.Parse(li[j + 1].InnerText.Replace("&nbsp;",""));
                                    break;
                                case "信息来源：":
                                    rent.FromType = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "房源编号：":
                                    rent.RentId =li[j + 1].SelectNodes("font")[0].InnerText.Trim();
                                    break;
                                case "区域方位：":
                                    rent.Region = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "小区地段：":
                                    rent.RentName = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "房屋类型：":
                                    rent.HouseType = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "户型结构：":
                                    rent.HouseHold = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "所在楼层：":
                                    rent.Floor = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "装修程度：":
                                    rent.Renovation = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "楼层总数：":
                                    rent.FloorCount = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "建筑面积：":
                                    rent.Area = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "车库情况：":
                                    rent.Garage = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "出租方式：":
                                    rent.RentMethod = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "房屋朝向：":
                                    rent.HouseOrientation = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "付款方式：":
                                    rent.PayType = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "建成年份：":
                                    rent.HouseYear = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "每月租金：":
                                    rent.RentPrice = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "配套设施：":
                                    rent.Facilities = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                case "备注说明：":
                                    rent.Remark = li[j + 1].InnerText.Replace("&nbsp;", "");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
               
            }
            return rent;
        }

        public List<Rent> GetRentList(string url)
        {
            var list = new List<Rent>();
            string html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("GB2312"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[3]/div[1]/div[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            if (reqObj != null)
            {
                var ulList = reqObj.SelectNodes(@"ul");
                for (int i = 1; i < ulList.Count - 1; i++)
                {
                    var li = ulList[i].SelectNodes("li");
                    if (li != null && li.Count > 0)
                    {
                        list.Add(GetRent(new Rent
                        {
                            ImgCount = (li[0].SelectNodes("img")[0].Attributes["src"].Value.IndexOf("0.gif") > 0 ? 0 : 1),
                            Url = "http://www.cxfccs.com" + li[1].SelectNodes("a")[0].Attributes["href"].Value.Trim()
                        }));
                    }
                }
            }
            Console.WriteLine(string.Format("本页获取信息目录[{0}]条", list.Count));
            return list;
        }
    }
}

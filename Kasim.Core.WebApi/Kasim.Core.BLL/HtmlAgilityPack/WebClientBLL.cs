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
* filename:WebClientBLL
* guid:4dc76182-1159-4e78-936f-a097a848dd9f
* auth:lip86
* date:2017-12-14 19:26:32
* desc:
*
*=====================================================================*/
using Kasim.Core.IBLL.HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class WebClientBLL : IWebClientBLL
    {
        public string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.Default);//注意编码
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }

        public string GetWebClient(string url, Encoding encoding)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, encoding);//注意编码
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }

        public string GetWordByBaiduAidemo(string imgUrl)
        {
            string url = "http://ai.baidu.com/aidemo";
            string body = "{\"type\":\"commontext\",\"image\":\"\",\"image_url\":\"" + imgUrl + "\"}";
            body = "{\"type\":\"commontext\",\"action\":\"getHeader\",\"image_url\":\"" + imgUrl + "\"}";
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                JObject jObject = (JObject)JsonConvert.DeserializeObject(result);
                StringBuilder str = new StringBuilder();
                foreach (var obj in jObject)
                {
                    str.Append(string.Format("Key:{0}   Value:{1}\r\n", obj.Key, obj.Value));
                    if (obj.Value is JObject)
                    {
                        foreach (var item in (JObject)obj.Value)
                        {
                            str.Append(string.Format("Key:{0}   Value:{1}\r\n", item.Key, item.Value));
                        }
                    }
                }
                var sb = str.ToString();
                return sb;
            }
        }
    }
}

﻿/**
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
** file：PbsClientBLL
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-01-31 13:11:14
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using Kasim.Core.Common;
using Kasim.Core.IBLL.WebApp;
using System.IO;
using System.Net;
using System.Text;

namespace Kasim.Core.BLL.WebApp
{
    public class PbsClientBLL : IPbsClientBLL
    {
        public string GetResponse(string url)
        {            
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/octet-stream; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {                
                return reader.ReadToEnd();
            }
        }

        public string PostWebClient(string url,string xml)
        {
            var client = new System.Net.WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var byteArray = System.Text.Encoding.UTF8.GetBytes(xml);
            var responseArray = client.UploadData(url, "POST", byteArray);
            return System.Text.Encoding.UTF8.GetString(WebDataZip.Decompress(responseArray));
        }
    }
}

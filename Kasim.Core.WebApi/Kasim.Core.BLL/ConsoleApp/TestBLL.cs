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

* Filename: TestBLL
* Namespace: Kasim.Core.BLL.ConsoleApp
* Classname: TestBLL
* Created: 2018-03-22 18:24:50
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class TestBLL
    {
        public void Test1()
        {
            string url = "http://www.51fapiao-nb.com/DZFPQT";
            var time = DateTime.Now.ToFileTimeUtc().ToString();
            Console.WriteLine(time);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("serviceName", "QtYzmValue");
            parameters.Add("content", "{\"appid\":\"\",\"yzm_key\":\"" + time + "\"}");
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder sbBody = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        sbBody.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        sbBody.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                Console.WriteLine(sbBody.ToString());
                byte[] buffer = encoding.GetBytes(sbBody.ToString());
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }

        private readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }
        private HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求  
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = DefaultUserAgent;
            //如果需要POST数据     
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                Console.WriteLine(buffer.ToString());
                byte[] data = charset.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }
        private string GetResponseReadToEnd(string url, IDictionary<string, string> parameters, Encoding encoding)
        {
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, encoding);
            //打印返回值  
            Stream stream = response.GetResponseStream();   //获取响应的字符串流  
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
            string html = sr.ReadToEnd();   //从头读到尾，放到字符串html 
            return html;
        }
        
        public void Test2()
        {
            string url = "http://www.51fapiao-nb.com/DZFPQT";
            Encoding encoding = Encoding.GetEncoding("utf-8");
            //获取验证码Key
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "serviceName", "QtYzm" },
                { "content", "{\"appid\":\"\"}" }
            };
            var result = GetResponseReadToEnd(url, parameters, encoding);
            Console.WriteLine("发票验证码Key:\r\n" + result);
            //获取验证码
            var jsonData = (JObject)JsonConvert.DeserializeObject(result);
            var yzm_key = jsonData["message"].Value<string>();
            parameters = new Dictionary<string, string>
            {
                { "serviceName", "QtYzmValue" },
                { "content", "{\"appid\":\"\",\"yzm_key\":\"" + yzm_key + "\"}" }
            };
            result = GetResponseReadToEnd(url, parameters, encoding);
            Console.WriteLine("发票验证码Val:\r\n"+result);
            System.Threading.Thread.Sleep(3000);
            //获取发票
            jsonData = (JObject)JsonConvert.DeserializeObject(result);
            var yzm_value = jsonData["message"].Value<string>();
            parameters = new Dictionary<string, string>
            {
                { "serviceName", "QtTycxNew" },
                { "content", "{\"fp_dm\":\"033021500111\"," +
                "\"fp_hm\":\"00237956\"," +
                "\"kphjje\":\"12.8\"," +
                "\"kprq\":\"2018-03-22\"," +
                "\"appid\":\"\"," +
                "\"yzm_key\":\"" + yzm_key + "\"," +
                "\"yzm_value\":\"" + yzm_value + "\"}" }
            };
            result = GetResponseReadToEnd(url, parameters, encoding);
            Console.WriteLine("发票密码:\r\n" + result);
            jsonData = (JObject)JsonConvert.DeserializeObject(result);
            var rc = jsonData["code"].Value<string>();
            //if (rc != "0000")
            //{
            //    Test2();                
            //}
        }
    }
}

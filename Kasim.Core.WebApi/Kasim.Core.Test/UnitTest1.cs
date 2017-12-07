using Kasim.Core.BLL.WebApi;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.Model.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Kasim.Core.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestWebRequestHttpGet()
        {
            string url = "http://localhost:20578/api/costimg/8145005";
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

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
                var sb=str.ToString();
                RequestModel requestModel = (RequestModel)JsonConvert.DeserializeObject(result, typeof(RequestModel));                
            }

        }

        [TestMethod]
        public void TestWebRequestHttpPost()
        {
            string url = "http://localhost:20578/api/costimg";
            string body = "{\"CostID\":\"8145005\"}";
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

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
                RequestModel requestModel = (RequestModel)JsonConvert.DeserializeObject(result, typeof(RequestModel));
            }

        }
    }

    #region "Test Class"
    class RequestModel
    {
        public string Result { get; set; }
        public CostImgs CostImg { get; set; }

        public class CostImgs
        {
            public int PicId { get; set; }
            public int ClientID { get; set; }
            public int Pbid { get; set; }
            public int CostID { get; set; }
            public string InvoiceCode { get; set; }
            public string InvoiceID { get; set; }
            public DateTime InvoiceDate { get; set; }
            public string PicUrl { get; set; }
            public string PicMD5 { get; set; }
            public DateTime AddDate { get; set; }

        }
    }

    
    #endregion
}

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
* namespace:Kasim.Core.Test
* filename:UnitTest2
* guid:2215ad7d-3536-4067-aef4-ec88d9949199
* auth:lip86
* date:2017-12-20 18:06:18
* desc:
*
*=====================================================================*/
using Kasim.Core.BLL.WebApi;
using Kasim.Core.IBLL.WebApi;
using Kasim.Core.Model.WebApi;
using Kasim.Core.Model.WebApi.ProductOffer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Kasim.Core.Test
{
    [TestClass]
    public class UnitTest2
    {
        
        public void TestProductOffer()
        {
            ConnectionStringOptions _conns = new ConnectionStringOptions {
                B2bConnection= "Data Source=192.168.215.4;database=BzOnlineMedicineOld;uid=sa;pwd=abc123"
            };
            IProductOfferBLL productOfferBLL = new ProductOfferBLL(_conns);
            List<ProductsWebOffer> list = productOfferBLL.ProductsWebOfferListById(18548);
        }

        /// <summary>
        /// 获取基础信息、库存信息、单据信息等
        /// </summary>
        [TestMethod]
        public void GetResponse()
        {
            var url = "http://115.231.58.130:8033/BRService.asmx/Action?n=45045b3d2a945736&m=GetClientInfoByClientName&p=BrServer&id=";
            var webRequest = System.Net.WebRequest.Create(url);
            webRequest.Timeout = 600000;
            var webResponse = webRequest.GetResponse();
            var stream = webResponse.GetResponseStream();
            var sourceBuffer = StreamToBytes(stream);
            var buffer = Decompress(sourceBuffer);
            var result = System.Text.Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// 提交要货单、返回配送获取状态
        /// </summary>
        [TestMethod]
        public void DoPost()
        {
            var url = "http://115.231.58.130:8033/BRService.asmx/Action?n=45045b3d2a945736&m=SubOrders&p=BrServer";
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><order><orderitems>"
                + "<orderitem><DetailID>1</DetailID><FID>1</FID><ClientID>1</ClientID><StoreID>1</StoreID><PID>1</PID><Quantity>1</Quantity></orderitem>"
                + "<orderitem><DetailID>2</DetailID><FID>1</FID><ClientID>1</ClientID><StoreID>1</StoreID><PID>2</PID><Quantity>1</Quantity></orderitem>"
                + "</orderitems></order>";
            var client = new System.Net.WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var byteArray = System.Text.Encoding.UTF8.GetBytes(xml);
            var responseArray = client.UploadData(url, "POST", byteArray);
            var result = System.Text.Encoding.UTF8.GetString(Decompress(responseArray));
        }
        #region "解压缩返回的数据"
        public byte[] Decompress(byte[] data)
        {
            try
            {
                var ms = new System.IO.MemoryStream(data);
                var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress, true);
                var msreader = new System.IO.MemoryStream();
                var buffer = new byte[4095];
                while (true)
                {
                    var reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0) break;
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] StreamToBytes(System.IO.Stream stream)
        {
            var bytes = new List<byte>();
            var temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add(Convert.ToByte(temp));
                temp = stream.ReadByte();
            }
            return bytes.ToArray();
        }
        #endregion

    }
}

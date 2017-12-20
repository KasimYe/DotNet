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
using System.Text;

namespace Kasim.Core.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestProductOffer()
        {
            ConnectionStringOptions _conns = new ConnectionStringOptions {
                B2bConnection= "Data Source=192.168.215.4;database=BzOnlineMedicineOld;uid=sa;pwd=BRYY@abc123"
            };
            IProductOfferBLL productOfferBLL = new ProductOfferBLL(_conns);
            List<ProductsWebOffer> list = productOfferBLL.ProductsWebOfferListById(1234);
        }
    }
}

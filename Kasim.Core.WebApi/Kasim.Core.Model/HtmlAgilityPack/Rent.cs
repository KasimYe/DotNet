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
 * namespace:Kasim.Core.Model.HtmlAgilityPack
 * filename:Rent
 * guid:eeee9a13-3fd8-4da5-a876-2dff779b28ed
 * auth:lip86
 * date:2018-02-23 19:47:05
 * desc:
 *
 *=====================================================================*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.Model.HtmlAgilityPack
{
    public class Rent
    {
        /// <summary>
        /// 房源编号
        /// </summary>
        public string RentId { get; set; }
        /// <summary>
        /// 信息来源
        /// </summary>
        public string FromType { get; set; }
        /// <summary>
        /// 区域方位
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 小区地段
        /// </summary>
        public string RentName { get; set; }
        /// <summary>
        /// 房屋类型
        /// </summary>
        public string HouseType { get; set; }
        /// <summary>
        /// 户型结构
        /// </summary>
        public string HouseHold { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public string Floor { get; set; }
        /// <summary>
        /// 楼层总数
        /// </summary>
        public string FloorCount { get; set; }
        /// <summary>
        /// 装修程度
        /// </summary>
        public string Renovation { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 车库情况
        /// </summary>
        public string Garage { get; set; }
        /// <summary>
        /// 出租方式
        /// </summary>
        public string RentMethod { get; set; }
        /// <summary>
        /// 房屋朝向
        /// </summary>
        public string HouseOrientation { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 建成年份
        /// </summary>
        public string HouseYear { get; set; }
        /// <summary>
        /// 每月租金
        /// </summary>
        public string RentPrice { get; set; }
        /// <summary>
        /// 配套设施
        /// </summary>
        public string Facilities { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 图片数量
        /// </summary>
        public int ImgCount { get; set; }
        /// <summary>
        /// 原文链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 信息更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}

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
* filename:ExcelExport
* guid:dee296a8-1d40-47b8-bf6b-1ff78ebe6b84
* auth:lip86
* date:2017-12-14 21:51:16
* desc:
*
*=====================================================================*/
using Kasim.Core.Model.HtmlAgilityPack;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class ExcelExport
    {
        public static void ExportB2C_111(List<B2C_111> list)
        {
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                //添加头
                worksheet.Cells[1, 1].Value = "产品信息&优惠信息&介绍";
                worksheet.Cells[1, 2].Value = "优惠组合价格";
                worksheet.Cells[1, 3].Value = "单价";
                int i = 2;
                foreach (var item in list)
                {
                    //添加值
                    worksheet.Cells["A"+ i.ToString()].Value = item.PName;
                    worksheet.Cells["B" + i.ToString()].Value = item.Total;
                    worksheet.Cells["C" + i.ToString()].Value = item.Price;
                    i++;
                }
                package.Save();
            }
        }
    }
}

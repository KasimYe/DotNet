/**
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
** file：ExcelExport
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-01-24 13:34:28
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Kasim.Core.Common
{
    public class ExcelExport
    {
        private static readonly int MAXROW = 1048575;
        public static void ExportByDataTable(DataTable dataTable, string workSheetName)
        {
            string sFileName = workSheetName + $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                int idxRow = 0;
                while (idxRow < dataTable.Rows.Count)
                {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(workSheetName + idxRow.ToString());
                    //添加头
                    for (int k = 1; k <= dataTable.Columns.Count; k++)
                    {
                        worksheet.Cells[1, k].Value = dataTable.Columns[k - 1].ColumnName;
                    }
                    Console.WriteLine("列名写入完毕");
                    int i = 2;
                    for (int j = idxRow; j < dataTable.Rows.Count; j++)
                    {
                        if (i < MAXROW)
                        {
                            //添加值
                            for (int l = 0; l < dataTable.Columns.Count; l++)
                            {
                                worksheet.Cells[Num2ABC(l) + i.ToString()].Value = dataTable.Rows[j][l];
                            }
                            Console.WriteLine(string.Format("…数据写入中… {0} / {1}", j + 1, dataTable.Rows.Count));
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    idxRow += i - 2;
                }

                package.Save();
            }
        }

        private static string Num2ABC(int i)
        {
            var abcArr = new string[] {
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
            return abcArr[i];
        }

        public static DataTable GetDataTable(string filePath, int worksheetIndex)
        {
            using (ExcelPackage package = new ExcelPackage(new FileStream(filePath, FileMode.Open)))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[worksheetIndex];
                DataTable dt = new DataTable();
                DataRow dr = null;
                int i = 0;
                for (int m = sheet.Dimension.Start.Row, n = sheet.Dimension.End.Row; m <= n; m++)
                {
                    if (i>0)
                    {
                        dr = dt.NewRow();
                    }
                    for (int j = sheet.Dimension.Start.Column, k = sheet.Dimension.End.Column; j <= k; j++)
                    {
                        string val = sheet.Cells[m, j].Value.ToString();                        
                        if (i==0)
                        {
                            if (string.IsNullOrEmpty(val))
                            {
                                k = j;
                            }
                            else
                            {
                                dt.Columns.Add(val, typeof(string));
                            }                            
                        }
                        else
                        {
                            dr[j-1] = val;
                        }                      
                    }
                    if (i>0)
                    {
                        dt.Rows.Add(dr);
                    }
                    i++;
                }
                return dt;
            }
        }
    }
}

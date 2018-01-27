using Kasim.Core.BLL.ConsoleApp;
using Kasim.Core.BLL.HtmlAgilityPack;
using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.Model.WebApi;
using System;

namespace Kasim.Core.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("*****************************************************************************************************");
            Console.WriteLine("******************************* Welcome to use Kasim.Core.ConsoleApp! *******************************");
            Console.WriteLine("*****************************************************************************************************");
            Console.WriteLine("************************************************ Menu ***********************************************");
            Console.WriteLine("*****************************************************************************************************");
            var conSet = new ConnectionStringOptions {
                DevConnection = "Data Source=192.168.0.2,1400;database=Bz_MIS;uid=sa;pwd=abc123"
            };
            Console.WriteLine("【1】重新计算后补返利设置");
            Console.WriteLine("【2】爬最新章节^o^!!!");
            Console.WriteLine("【3】根据SQL语句导出数据到Excel");
            Console.WriteLine("请输入对应菜单数字：");
            var keyCode = Console.ReadLine();
            while (keyCode.ToUpper()!="EXIT")
            {
                switch (keyCode)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("请输入返利设置ID：");
                            ISupplierReturnBLL supplierReturnBLL = new SupplierReturnBLL(conSet);
                            var sRSCID = int.Parse(Console.ReadLine());
                            supplierReturnBLL.SupplySupplierReturn(sRSCID);                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);                            
                        }                        
                        break;
                    case "2":
                        try
                        {

                            Console.WriteLine("请输入书本目录地址：");
                            var menuUrl = Console.ReadLine();
                            Console.WriteLine("请输入书本内容父级地址：");
                            var contentHeadUrl = Console.ReadLine();
                            Console.WriteLine("请输入书本名称：");
                            var bookName = Console.ReadLine();
                            Console.WriteLine("请输入章节名称：");
                            var title = Console.ReadLine();
                            IBookTxtBLL bookTxtBLL = new BookTxtBLL_shubaowa {
                                MenuUrl=menuUrl,
                                ContentHeadUrl=contentHeadUrl,
                                BookName=bookName
                            };                            
                            bookTxtBLL.DownloadBook(title);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("请输入工作簿名称：");
                            var workSheetName = Console.ReadLine();
                            Console.WriteLine("请输入查询语句(不能超过26列)：");
                            var goCode = Console.ReadLine();
                            var sql = "";
                            while (goCode.ToUpper()!="GO")
                            {
                                sql += " "+goCode;
                                goCode = Console.ReadLine();
                            }
                            IExportSqlBLL exportSqlBLL = new ExportSqlBLL(conSet);                            
                            exportSqlBLL.ExportBySqlServer(workSheetName, sql);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("请输入对应菜单数字：");
                        break;
                }
                keyCode = Console.ReadLine();
            }           
        }        
    }
}

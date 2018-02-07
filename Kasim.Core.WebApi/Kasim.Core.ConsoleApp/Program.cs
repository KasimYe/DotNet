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
                DevConnection = "Data Source=192.168.0.210;database=Bz_MIS;uid=sa;pwd=BRYY@abc123",//"Data Source=192.168.0.2,1400;database=Bz_MIS;uid=sa;pwd=abc123",
                TaxConnection= "Data Source=192.168.0.210;database=Bz_MIS;uid=sa;pwd=BRYY@abc123"
            };
            Console.WriteLine("【1】重新计算后补返利设置");
            Console.WriteLine("【2】爬最新章节^o^!!!");
            Console.WriteLine("【3】根据SQL语句导出数据到Excel");
            Console.WriteLine("【4】爬酷我音乐的歌,盗版万岁^o^!!!");
            Console.WriteLine("【5】删除主从表大数据");
            Console.WriteLine("【6】删除临时表T_开头");
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
                            IBookTxtBLL bookTxtBLL = null;
                            switch (menuUrl.Split('.')[1].ToUpper())
                            {
                                case "SJTXT":
                                    bookTxtBLL = new BookTxtBLL_sjtxt
                                    {
                                        MenuUrl = menuUrl,
                                        ContentHeadUrl = contentHeadUrl,
                                        BookName = bookName
                                    };
                                    break;
                                case "PAOMOV":
                                    bookTxtBLL = new BookTxtBLL_paomov
                                    {
                                        MenuUrl = menuUrl,
                                        ContentHeadUrl = contentHeadUrl,
                                        BookName = bookName
                                    };
                                    break;
                                case "SHUBAOWA":
                                    bookTxtBLL = new BookTxtBLL_shubaowa
                                    {
                                        MenuUrl = menuUrl,
                                        ContentHeadUrl = contentHeadUrl,
                                        BookName = bookName
                                    };
                                    break;
                                case "7MXS":
                                    bookTxtBLL = new BookTxtBLL_7mxs
                                    {
                                        MenuUrl = menuUrl,
                                        ContentHeadUrl = contentHeadUrl,
                                        BookName = bookName
                                    };
                                    break;
                                case "HRSX8":
                                    bookTxtBLL = new BookTxtBLL_hrsx8
                                    {
                                        MenuUrl = menuUrl,
                                        ContentHeadUrl = contentHeadUrl,
                                        BookName = bookName
                                    };
                                    break;
                                default:
                                    break;
                            }
                            if (bookTxtBLL!=null)
                            {
                                bookTxtBLL.DownloadBook(title);
                            }                            
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
                    case "4":
                        Console.WriteLine("请输入歌手名字：");
                        var artist = Console.ReadLine();
                        Console.WriteLine("请输入歌名：");
                        var name = Console.ReadLine();
                        IMusicDownBLL musicDownBLL = new MusicDownBLL();
                        musicDownBLL.DownMusic(artist, name);
                        break;
                    case "5":
                        Console.WriteLine("请输入主表名：");
                        var formTable = Console.ReadLine();
                        Console.WriteLine("请输入子表名：");
                        var detailTable = Console.ReadLine();
                        Console.WriteLine("请输入主键字段名：");
                        var primaryKey = Console.ReadLine();
                        IDeleteTableDataBLL deleteTableDataBLL = new DeleteTableDataBLL(conSet);
                        deleteTableDataBLL.DeleteTable(formTable, detailTable, primaryKey);
                        break;
                    case "6":
                        IDeleteTableDataBLL deleteTableDataBLL2 = new DeleteTableDataBLL(conSet);
                        deleteTableDataBLL2.DropTable();
                        break;
                    case "test":
                        try
                        {
                            IWebClientBLL webClientBLL = new WebClientBLL();
                            Console.WriteLine(webClientBLL.GetWordByBaiduAidemo("http://www.7mxs.cc/image/fei.jpg"));
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

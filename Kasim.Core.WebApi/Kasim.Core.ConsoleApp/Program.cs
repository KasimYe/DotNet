using Kasim.Core.BLL.ConsoleApp;
using Kasim.Core.IBLL.ConsoleApp;
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
                    default:
                        Console.WriteLine("请输入对应菜单数字：");
                        break;
                }
                keyCode = Console.ReadLine();
            }           
        }        
    }
}

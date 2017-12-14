using Kasim.Core.BLL.HtmlAgilityPack;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.HtmlAgilityPackApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebClientBLL webClientBLL = new WebClientBLL();
            IB2C_111_BLL b2C_111_BLL = new B2C_111_BLL();
            DateTime startTime = DateTime.Now;
            string url = "http://www.111.com.cn/categories/953710-j1.html";
            string html = webClientBLL.GetWebClient(url);
            int pageCount = b2C_111_BLL.GetPageCountByFirstPage(html);
            Console.WriteLine(string.Format("****************************************获取总页数共【{0}】页****************************************", pageCount));
            //pageCount = 1;
            List<B2C_111> list = new List<B2C_111>();
            for (int i = 1; i < pageCount + 1; i++)
            {
                url = string.Format("http://www.111.com.cn/categories/953710-j{0}.html", i);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("GBK"));
                Console.WriteLine(string.Format("****************************************当前为第【{0}】页****************************************", i));
                var result = b2C_111_BLL.ParsePageByProductType(html);
                list.AddRange(result);
                Console.WriteLine(string.Format("****************************************第【{0}】页,抓取完毕****************************************", i));
            }
            TimeSpan ts = DateTime.Now.Subtract(startTime);
            Console.WriteLine(string.Format("****************************************共耗时：{0}****************************************", ts.ToString()));
            ExcelExport.ExportB2C_111(list);
            Console.WriteLine("****************************************导出成功****************************************");
            Console.ReadKey();
        }
    }
}

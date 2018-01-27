using HtmlAgilityPack;
using Kasim.Core.Common;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class BookTxtBLL_shubaowa : IBookTxtBLL
    {
        public string MenuUrl { get; set; }
        public string ContentHeadUrl { get; set; }
        public string BookName { get; set; }

        IWebClientBLL webClientBLL;
        public BookTxtBLL_shubaowa()
        {
            webClientBLL = new WebClientBLL();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void DownloadBook(string title)
        {
            var menuList = GetMenuList();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BookName + ".txt");//江山风月剑
            if (string.IsNullOrEmpty(title))
            {
                foreach (var book in menuList)
                {
                    FileOperate.WriteFile(path, GetContent(book).Content);
                }
                Console.WriteLine(string.Format("全部章节下载完毕，文件地址：{0}", path));
            }
            else
            {
                menuList.Where(x => x.Title == title).ToList()
                    .ForEach((x) =>
                    {
                        FileOperate.WriteFile(path, GetContent(x).Content);
                    });
                Console.WriteLine(string.Format("最新章节下载完毕，文件地址：{0}", path));
            }
        }

        public Book GetContent(Book book)
        {
            try
            {
                string url = book.Url;
                string html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("GBK"));
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var xPath = @"/html[1]/body[1]/div[1]/div[3]/div[1]/div[3]";
                var reqObj = document.DocumentNode.SelectSingleNode(xPath);
                if (reqObj != null)
                {
                    book.Content = reqObj.InnerText.Replace("&nbsp;", " ");
                }
                Console.WriteLine(string.Format("获取章节内容：[{0}] 成功！", book.Title));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("获取章节内容：[{0}] 失败，{1}", book.Title, ex.Message));
                System.Threading.Thread.Sleep(3000);
                GetContent(book);
            }
            return book;
        }

        public List<Book> GetMenuList()
        {
            //string url = "http://www.shubaowa.com/0_814/";
            string html = webClientBLL.GetWebClient(MenuUrl, Encoding.GetEncoding("GBK"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/body[1]/div[1]/div[5]/div[1]/dl[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            var list = new List<Book>();
            if (reqObj != null)
            {
                var parentNodeList = reqObj.SelectNodes(@"dd");
                var i = 1;
                foreach (var li in parentNodeList)
                {
                    var a = li.SelectNodes(@"a")[0];
                    var name = a.InnerText.Trim();
                    //var uurl = "http://www.shubaowa.com" + a.Attributes["href"].Value.Trim();
                    var uurl = ContentHeadUrl + a.Attributes["href"].Value.Trim();
                    list.Add(new Book()
                    {
                        Id = i,
                        Title = name,
                        Url = uurl
                    });
                    i++;
                }
                Console.WriteLine(string.Format("获取章节完毕：共找到[{0}]个章节", i));
            }
            return list;
        }
    }
}

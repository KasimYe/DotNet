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
** file：BookTxtBLL_ppxs
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-02-18 21:34:30
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

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
    public class BookTxtBLL_ppxs: IBookTxtBLL
    {
        public string MenuUrl { get; set; }
        public string ContentHeadUrl { get; set; }
        public string BookName { get; set; }

        IWebClientBLL webClientBLL;
        public BookTxtBLL_ppxs()
        {
            webClientBLL = new WebClientBLL();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public List<Book> GetMenuList()
        {
            //string url = "http://www.paomov.com/txt91435.shtml";
            string html = webClientBLL.GetWebClient(MenuUrl, Encoding.GetEncoding("GBK"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[5]/div[1]/div[1]/div[4]/div[2]/div[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            var list = new List<Book>();
            if (reqObj != null)
            {
                var parentNodeList = reqObj.SelectNodes(@"li");
                var i = 1;
                foreach (var li in parentNodeList)
                {
                    var a = li.SelectNodes(@"a")[0];
                    var name = a.InnerText.Trim();
                    //var uurl = "http://www.paomov.com" + a.Attributes["href"].Value.Trim();
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

        public Book GetContent(Book book)
        {
            try
            {
                string url = book.Url;
                string html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("GBK"));
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var xPath = @"/html[1]/body[1]/div[4]/div[2]/div[2]";
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

        public void DownloadBook(string title)
        {
            var menuList = GetMenuList();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TXT", BookName + ".txt");//"大唐妖孽.txt"
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
    }
}

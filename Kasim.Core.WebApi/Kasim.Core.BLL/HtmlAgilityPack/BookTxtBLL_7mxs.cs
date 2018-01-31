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
** file：BookTxtBLL_7mxs
** desc：
** 
** auth：KasimYe (KASIM)
** date：2018-01-30 18:56:05
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
    public class BookTxtBLL_7mxs : IBookTxtBLL
    {
        public string MenuUrl { get; set; }
        public string ContentHeadUrl { get; set; }
        public string BookName { get; set; }

        IWebClientBLL webClientBLL;
        public BookTxtBLL_7mxs()
        {
            webClientBLL = new WebClientBLL();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void DownloadBook(string title)
        {
            var menuList = GetMenuList();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TXT", BookName + ".txt");//"家庭幻想.txt"
            if (string.IsNullOrEmpty(title))
            {
                foreach (var book in menuList)
                {
                    FileOperate.WriteFile(path, book.Title);
                    FileOperate.WriteFile(path, GetContent(book).Content);
                }
                Console.WriteLine(string.Format("全部章节下载完毕，文件地址：{0}", path));
            }
            else
            {
                menuList.Where(x => x.Title == title).ToList()
                    .ForEach((x) =>
                    {
                        FileOperate.WriteFile(path, x.Title);
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
                string html = webClientBLL.GetWebClient(url, Encoding.GetEncoding("gb2312"));
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
            //string url = "http://www.sjtxt.la/book/70556/";
            string html = webClientBLL.GetWebClient(MenuUrl, Encoding.GetEncoding("gb2312"));
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/table[4]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            var list = new List<Book>();
            if (reqObj != null)
            {
                var parentTrList = reqObj.SelectNodes(@"tr");
                var i = 1;
                for (int tr = 1; tr < parentTrList.Count; tr++)
                {
                    var parentTdList = parentTrList[tr].SelectNodes(@"td");
                    for (int td = 0; td < parentTdList.Count; td ++)
                    {
                        if (parentTdList[td].SelectNodes(@"div") != null)
                        {
                            var aas = parentTdList[td].SelectNodes(@"div")[0].SelectNodes(@"a");
                            if (aas != null && aas.Count > 0)
                            {
                                var a = aas[0];
                                var name = a.InnerText.Trim();
                                //var uurl = url + a.Attributes["href"].Value.Trim();
                                var uurl = ContentHeadUrl + a.Attributes["href"].Value.Trim();
                                list.Add(new Book()
                                {
                                    Id = i,
                                    Title = name,
                                    Url = uurl
                                });
                                i++;
                            }
                        }
                    }
                    for (int td = 0; td < parentTdList.Count; td ++)
                    {
                        if (parentTdList[td].SelectNodes(@"div") != null)
                        {
                            var aas = parentTdList[td].SelectNodes(@"div")[1].SelectNodes(@"a");
                            if (aas!=null && aas.Count > 0)
                            {
                                var a = aas[0];
                                var name = a.InnerText.Trim();
                                //var uurl = url + a.Attributes["href"].Value.Trim();
                                var uurl = ContentHeadUrl + a.Attributes["href"].Value.Trim();
                                list.Add(new Book()
                                {
                                    Id = i,
                                    Title = name,
                                    Url = uurl
                                });
                                i++;
                            }
                        }
                    }
                }
                Console.WriteLine(string.Format("获取章节完毕：共找到[{0}]个章节", i));
            }
            return list;
        }
    }
}

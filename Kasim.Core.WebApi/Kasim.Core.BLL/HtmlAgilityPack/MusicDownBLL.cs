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
* namespace:Kasim.Core.BLL.HtmlAgilityPack
* filename:MusicDownBLL
* guid:4f3b470b-a2de-4257-ab2d-90e5f080c703
* auth:lip86
* date:2018-02-02 19:55:10
* desc:
*
*=====================================================================*/
using HtmlAgilityPack;
using Kasim.Core.IBLL.HtmlAgilityPack;
using Kasim.Core.Model.HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Kasim.Core.Common;
using System.Linq;

namespace Kasim.Core.BLL.HtmlAgilityPack
{
    public class MusicDownBLL : IMusicDownBLL
    {
        IWebClientBLL webClientBLL;
        public MusicDownBLL()
        {
            webClientBLL = new WebClientBLL();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private ParentMusic parentMusic = null;
        private int errTime = 0;
        public void DownMusic(string artist, string name)
        {
            var url = "http://www.kuwo.cn/artist/content?name=" + artist;
            string html = webClientBLL.GetWebClient(url, Encoding.UTF8);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/html[1]/body[1]/div[4]/div[2]/div[2]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            if (reqObj != null)
            {
                var ul = reqObj.SelectNodes(@"ul")[0];
                parentMusic = new ParentMusic
                {
                    Page = int.Parse(ul.Attributes["data-page"].Value),
                    Rn = int.Parse(ul.Attributes["data-rn"].Value),
                    Pn = 0
                };
                Console.WriteLine(string.Format("获取歌曲页码完毕：共[{0}]页，每页[{1}]首歌", parentMusic.Page, parentMusic.Rn));
            }
            xPath = @"/html[1]/body[1]/div[4]";
            reqObj = document.DocumentNode.SelectSingleNode(xPath);
            if (reqObj != null)
            {
                var div = reqObj.SelectNodes(@"div")[0];
                parentMusic.ArtistId = div.Attributes["data-artistid"].Value;
                Console.WriteLine(string.Format("获取歌手ID成功：[{0}]", parentMusic.ArtistId));
            }
            for (int i = 0; i < parentMusic.Page; i++)
            {
                url = string.Format("http://www.kuwo.cn/artist/contentMusicsAjax?artistId={0}&pn={1}&rn={2}", parentMusic.ArtistId, i, parentMusic.Rn);
                GetParentMusic(url);
            }
            foreach (var m in (string.IsNullOrEmpty(name) ? parentMusic.ListMusic : parentMusic.ListMusic.Where(x => x.Name.Contains(name)).ToList()))
            {
                GetMusic(m);
            }
            Console.WriteLine(string.Format("下载完毕，共下载歌曲：[{0}]首", parentMusic.Page * parentMusic.Rn));
        }

        public void GetMusic(Music music)
        {
            try
            {
                var url = string.Format("http://antiserver.kuwo.cn/anti.s?format=mp3&rid={0}&type=convert_url&response=res", music.Id);
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MUSIC", music.Artist, music.Album, string.Format("{0} - {1}.mp3", music.Artist, music.Name));
                WebDownload.DownLoad(url, path);
                Console.WriteLine(string.Format("歌曲：[{0}]，专辑：[{1}]，下载成功！", music.Name, music.Album));
            }
            catch
            {
                if (errTime < 4)
                {
                    errTime++;
                    GetMusic(music);
                }
                else
                {
                    errTime = 0;
                }
            }
        }

        public void GetParentMusic(string url)
        {
            string html = webClientBLL.GetWebClient(url, Encoding.UTF8);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xPath = @"/ul[1]";
            var reqObj = document.DocumentNode.SelectSingleNode(xPath);
            var list = new List<Music>();
            if (reqObj != null)
            {
                var parentNodeList = reqObj.SelectNodes(@"li");
                var i = 0;
                foreach (var li in parentNodeList)
                {
                    var a = li.SelectNodes("div")[0].SelectNodes("a")[0];
                    var uurl = "http://www.kuwo.cn" + a.Attributes["href"].Value.Trim();
                    var div = li.SelectNodes("div")[3].SelectNodes("div")[0];
                    var musicJson = div.Attributes["data-music"].Value;
                    JObject jObject = (JObject)JsonConvert.DeserializeObject(musicJson);
                    list.Add(new Music()
                    {
                        Id = jObject["id"].ToString(),
                        Name = jObject["name"].ToString(),
                        Artist = jObject["artist"].ToString(),
                        Album = jObject["album"].ToString(),
                        Pay = jObject["pay"].ToString(),
                        Url = uurl
                    });
                    i++;
                }
                if (parentMusic.ListMusic == null)
                {
                    parentMusic.ListMusic = list;
                }
                else
                {
                    parentMusic.ListMusic.AddRange(list);
                }
                Console.WriteLine(string.Format("获取歌曲列表完毕：共收集[{0}]首歌曲信息", i));
            }
        }
    }
}

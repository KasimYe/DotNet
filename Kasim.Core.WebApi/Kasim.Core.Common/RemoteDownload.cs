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
* namespace:Kasim.Core.Common
* filename:RemoteDownload
* guid:eb606815-9828-4822-be76-7989fda98fcd
* auth:lip86
* date:2018-02-02 21:31:41
* desc:
*
*=====================================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Kasim.Core.Common
{

    public class WebDownload
    {
        public async static void DownLoadAsync(string Url, string FileName)
        {          
            


            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(Url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    try
                    {
                        var path = Path.GetDirectoryName(FileName);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("{0} \r\n {1}", FileName, ex.Message));
                        return;
                    }

                    byte[] buffer = new byte[1024];
                    if (File.Exists(FileName))
                        File.Delete(FileName);
                    Stream outStream = File.Create(FileName);
                    int l;
                    do
                    {
                        l = stream.Read(buffer, 0, buffer.Length);
                        if (l > 0)
                            outStream.Write(buffer, 0, l);
                    }
                    while (l > 0);
                    outStream.Close();
                    stream.Close();
                    Console.WriteLine(string.Format("***** 歌曲下载成功！ *****\r\n{0}", FileName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} \r\n {1}", FileName, ex.Message));
            }
        }

        public static void DownLoad(string Url, string FileName)
        {
            try
            {
                var path = Path.GetDirectoryName(FileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} \r\n {1}", FileName, ex.Message));
                return;
            }
            bool Value = false;
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    Value = SaveBinaryFile(response, FileName);
                }
            }
            catch (Exception err)
            {
                string aa = err.ToString();
                throw;
            }
        }
        /// <summary>
        /// 下载不重命名
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="DirName">文件夹路径</param>
        public static string DownLoad2(string Url, string DirName)
        {
            try
            {
                var path = Path.GetDirectoryName(DirName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} \r\n {1}", DirName, ex.Message));
                return ex.Message;
            }
            bool Value = false;
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                response = request.GetResponse();
                stream = response.GetResponseStream();
                var cD = response.Headers["Content-Disposition"];
                var filename = cD.Substring(cD.IndexOf("filename=")+9);
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    Value = SaveBinaryFile(response,Path.Combine(DirName, filename));
                    return filename;
                }
                return "";
            }
            catch (Exception err)
            {
                string aa = err.ToString();
                return err.Message;
            }
        }

        /// <summary>
        /// Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // 将二进制文件保存到磁盘
        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];
            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);
                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
                throw;
            }
            return Value;
        }
    }
}

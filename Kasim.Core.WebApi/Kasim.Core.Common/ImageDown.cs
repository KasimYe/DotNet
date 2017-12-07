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
** file：ImageDown
** desc：
** 
** auth：KasimYe (KASIM)
** date：2017-12-05 15:25:23
**
** Ver.：V1.0.0
**----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Kasim.Core.Common
{
    /// <summary>
    /// 图片下载
    /// </summary>
    public class ImageDown
    {
        public ImageDown()
        { }

        #region 私有方法
        /// <summary>
        /// 获取图片标志
        /// </summary>
        private string[] GetImgTag(string htmlStr)
        {
            Regex regObj = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strAry = new string[regObj.Matches(htmlStr).Count];
            int i = 0;
            foreach (Match matchItem in regObj.Matches(htmlStr))
            {
                strAry[i] = GetImgUrl(matchItem.Value);
                i++;
            }
            return strAry;
        }

        /// <summary>
        /// 获取图片URL地址
        /// </summary>
        private string GetImgUrl(string imgTagStr)
        {
            string str = "";
            Regex regObj = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(imgTagStr))
            {
                str = matchItem.Value;
            }
            return str;
        }
        #endregion

        /// <summary>
        /// 下载图片到本地
        /// </summary>
        /// <param name="strHTML">HTML</param>
        /// <param name="path">路径</param>
        /// <param name="nowyymm">年月</param>
        /// <param name="nowdd">日</param>
        public string SaveUrlPics(string strHTML, string path)
        {
            string nowym = DateTime.Now.ToString("yyyy-MM");  //当前年月
            string nowdd = DateTime.Now.ToString("dd");       //当天号数
            path = path + nowym + "/" + nowdd;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] imgurlAry = GetImgTag(strHTML);
            try
            {
                for (int i = 0; i < imgurlAry.Length; i++)
                {
                    string preStr = System.DateTime.Now.ToString() + "_";
                    preStr = preStr.Replace("-", "");
                    preStr = preStr.Replace(":", "");
                    preStr = preStr.Replace(" ", "");
                    WebClient wc = new WebClient();
                    wc.DownloadFile(imgurlAry[i], path + "/" + preStr + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf("/") + 1));
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return strHTML;
        }

        public void PicRemove(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void PicSave(Image image, string path)
        {
            string dirPath = path.Remove(path.LastIndexOf('/') + 1);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            image.Save(path, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 下载图片并返回
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <returns>返回图片</returns>
        public Image DownUrlPic(string url)
        {
            try
            {
                WebRequest myrequest = WebRequest.Create(url);
                WebResponse myresponse = myrequest.GetResponse();
                Stream imgstream = myresponse.GetResponseStream();
                Image img = Image.FromStream(imgstream);
                return img;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 图片转内存流
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>内存流</returns>
        public MemoryStream ImageToMemoryStream(Image image)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                //image.Save(@"C:\Users\lip86\Desktop\test\123.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                image.Save(ms, ImageFormat.Jpeg);
                return ms;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// Convert Byte[] to a picture and Store it in file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string CreateImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            Image image = BytesToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            File.WriteAllBytes(file, buffer);
            return file;
        }
    }
}

/*
                 ___====-_  _-====___
           _--^^^#####//      \\#####^^^--_
        _-^##########// (    ) \\##########^-_
       -############//  |\^^/|  \\############-
     _/############//   (@::@)   \\############\_
    /#############((     \\//     ))#############\
   -###############\\    (oo)    //###############-
  -#################\\  / VV \  //#################-
 -###################\\/      \//###################-
_#/|##########/\######(   /\   )######/\##########|\#_
|/ |#/\#/\#/\/  \#/\##\  |  |  /##/\#/  \/\#/\#/\#| \|
`  |/  V  V  `   V  \#\| |  | |/#/  V   '  V  V  \|  '
   `   `  `      `   / | |  | | \   '      '  '   '
                    (  | |  | |  )
                   __\ | |  | | /__
                  (vvv(VVV)(VVV)vvv)                  

* Filename: WeixinTemplate_QhProduct
* Namespace: Kasim.Core.WeiXin.MP.CommonService.TemplateMessage
* Classname: WeixinTemplate_QhProduct
* Created: 2018-05-04 10:42:05
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.WeiXin.MP.CommonService.TemplateMessage
{
    /// <summary>
    /// 缺货模板消息
    /// </summary>
    public class WeixinTemplate_QhProduct : TemplateMessageBase
    {
        const string TEMPLATE_ID = "gnxYnjhWtFJaDpR3gk4oC-965LY-hpHpOSn5bJnugbw";//每个公众号都不同，需要根据实际情况修改

        public TemplateDataItem first { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        public TemplateDataItem keyword1 { get; set; }
        /// <summary>
        /// Host
        /// </summary>
        public TemplateDataItem keyword2 { get; set; }
        /// <summary>
        /// Service
        /// </summary>
        public TemplateDataItem keyword3 { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        //public TemplateDataItem keyword4 { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        //public TemplateDataItem keyword5 { get; set; }

        public TemplateDataItem remark { get; set; }

        public WeixinTemplate_QhProduct(string saler, string pname, string qty, string indate,
            string _remark, string url = null, string templateId = TEMPLATE_ID) 
            : base(templateId, url, "缺货商品到货提醒")
        {
            first = new TemplateDataItem(saler, "#BB5500");
            keyword1 = new TemplateDataItem(pname, "#BB5500");
            keyword2 = new TemplateDataItem(qty, "#BB5500");
            keyword3 = new TemplateDataItem(indate, "#BB5500");
            //keyword4 = new TemplateDataItem(status);
            //keyword5 = new TemplateDataItem(message);
            remark = new TemplateDataItem(_remark, "#BB5500");
        }
    }
}

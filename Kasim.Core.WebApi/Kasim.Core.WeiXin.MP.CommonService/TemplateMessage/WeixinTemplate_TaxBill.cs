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

* Filename: WeixinTemplate_TaxBill
* Namespace: Kasim.Core.WeiXin.MP.CommonService.TemplateMessage
* Classname: WeixinTemplate_TaxBill
* Created: 2018-05-04 14:39:38
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
    public class WeixinTemplate_TaxBill : TemplateMessageBase
    {
        const string TEMPLATE_ID = "dVtVpfZyHHlgo098R6jGvwwnHbs5CzpNZAJ21cNKqUg";//每个公众号都不同，需要根据实际情况修改

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
        public TemplateDataItem keyword4 { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public TemplateDataItem keyword5 { get; set; }

        public TemplateDataItem remark { get; set; }

        public WeixinTemplate_TaxBill(string saler, string cleintName, string taxTotal, string fpdm, string kqrq,
            string _remark, string url = null, string templateId = TEMPLATE_ID)
            : base(templateId, url, "发票开具通知")
        {
            first = new TemplateDataItem(saler, "#698B22");
            keyword1 = new TemplateDataItem(cleintName, "#698B22");
            keyword2 = new TemplateDataItem("浙江弘瑞医药有限公司", "#698B22");
            keyword3 = new TemplateDataItem(taxTotal, "#698B22");
            keyword4 = new TemplateDataItem(fpdm, "#698B22");
            keyword5 = new TemplateDataItem(kqrq, "#698B22");
            remark = new TemplateDataItem(_remark, "#698B22");
        }
    }
}

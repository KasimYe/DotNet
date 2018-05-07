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

* Filename: UserAction
* Namespace: Kasim.Core.Model.Weixin.MP
* Classname: UserAction
* Created: 2018-05-04 10:08:38
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kasim.Core.Model.Weixin.MP
{
    public class UserAction
    {
        public decimal AccessId { get; set; }
        public decimal WxUserId { get; set; }
        public decimal KeyId { get; set; }
        public string OpenId { get; set; }
        [DisplayName("昵称")]
        public string NickName { get; set; }
        [DisplayName("性别")]
        public string Sex { get; set; }
        [DisplayName("城市")]
        public string City { get; set; }
        [DisplayName("省份")]
        public string Province { get; set; }
        [DisplayName("国家")]
        public string Country { get; set; }
        [DisplayName("功能名称")]
        public string KeyWord { get; set; }
        [DisplayName("申请时间")]
        public DateTime AddTime { get; set; }
        [DisplayName("联系电话")]
        public string ApplyTel { get; set; }
        [DisplayName("备注")]
        public string ApplyNote { get; set; }
        [DisplayName("审核时间")]
        public DateTime? ValidTime { get; set; }
        [DisplayName("启用")]
        public bool IsUsed { get; set; }
        public string Language { get; set; }
        public DateTime SubscribeTime { get; set; }
    }
}

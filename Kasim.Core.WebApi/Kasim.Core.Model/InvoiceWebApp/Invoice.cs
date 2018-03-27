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

* Filename: Invoice
* Namespace: Kasim.Core.Model.InvoiceWebApp
* Classname: Invoice
* Created: 2018-03-22 20:57:01
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kasim.Core.Model.InvoiceWebApp
{
    public class Invoice
    {        
        [DisplayName("流水号")]
        public string FormNumber { get; set; }
        [DisplayName("流水日期")]
        [DataType(DataType.Date)]
        public DateTime SystemDate { get; set; }
        [DisplayName("客户名称")]
        public string ClientName { get; set; }
        [DisplayName("发票号码")]
        public string InvoiceCode { get; set; }
        [DisplayName("发票代码")]
        public string InvoiceId { get; set; }
        [DisplayName("开票日期")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DisplayName("发票金额")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal InvoiceSum { get; set; }
        [DisplayName("文件名")]
        public string PdfFileName { get; set; }
    }
}

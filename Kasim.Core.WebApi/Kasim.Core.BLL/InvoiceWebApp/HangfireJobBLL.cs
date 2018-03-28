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

* Filename: HangfireJobBLL
* Namespace: Kasim.Core.BLL.InvoiceWebApp
* Classname: HangfireJobBLL
* Created: 2018-03-28 13:11:02
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.BLL.InvoiceWebApp
{
    public class HangfireJobBLL : IHangfireJobBLL
    {
        public static ConnectionStringOptions conns;
        public void DownloadInvoices()
        {
            IInvoiceBLL bll = new InvoiceBLL(conns);
            var invoice = bll.GetNewInvoice();
            bll.DownloadInvoicePdf(invoice);
        }
    }
}

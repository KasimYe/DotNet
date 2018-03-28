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

* Filename: IHangfireJobBLL
* Namespace: Kasim.Core.IBLL.InvoiceWebApp
* Classname: IHangfireJobBLL
* Created: 2018-03-28 13:10:50
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.Model.InvoiceWebApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasim.Core.IBLL.InvoiceWebApp
{
    public interface IHangfireJobBLL
    {
        void DownloadInvoices();
    }
}

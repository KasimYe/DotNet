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

* Filename: HongRuiBLL
* Namespace: Kasim.Core.BLL.ConsoleApp
* Classname: HongRuiBLL
* Created: 2018-05-16 22:39:37
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using Kasim.Core.IBLL.ConsoleApp;
using Kasim.Core.IDAL.ConsoleApp;
using Kasim.Core.SQLServerDAL.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kasim.Core.BLL.ConsoleApp
{
    public class HongRuiBLL: IHongRuiBLL
    {
        private readonly IHongRuiDAL dal = new HongRuiDAL();

        public void Start()
        {
            Console.WriteLine("输入麦斯康莱系统中弘瑞的ClientID:");
            var hrClientId = Console.ReadLine();
            var dt=GetSaleBills(Convert.ToInt32(hrClientId));
        }

        private DataTable GetSaleBills(int clientId)
        {
            return dal.GetSaleBills(clientId);
        }
    }
}

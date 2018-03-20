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

* Filename: ClientBLL
* Namespace: Kasim.Core.Redis.WebApi.BLL
* Classname: ClientBLL
* Created: 2018-03-20 22:44:59
* Author: KasimYe
* Ps: For My Son YH
* Description: 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kasim.Core.Redis.WebApi.Factory;
using Kasim.Core.Redis.WebApi.Model;

namespace Kasim.Core.Redis.WebApi.BLL
{
    public class ClientBLL
    {
        DAL.Redis.ClientDAL dal;
        public ClientBLL(ConnectionStringOptions value)
        {
            Conf.ConStrOps = value;
            dal = new DAL.Redis.ClientDAL();
        }

        public List<Client> GetClients()
        {
            return dal.GetList();
        }

        internal object GetClient(int id)
        {
            throw new NotImplementedException();
        }
    }
}

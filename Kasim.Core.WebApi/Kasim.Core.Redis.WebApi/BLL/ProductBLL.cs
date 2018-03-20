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

* Filename: ProductBLL
* Namespace: Kasim.Core.Redis.WebApi.BLL
* Classname: ProductBLL
* Created: 2018-03-20 22:54:02
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
    public class ProductBLL
    {
        DAL.Redis.ProductDAL dal;
        public ProductBLL(ConnectionStringOptions value)
        {
            Conf.ConStrOps = value;
            dal = new DAL.Redis.ProductDAL();
        }

        public List<Product> GetProducts()
        {
            return dal.GetList();
        }

        internal object GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}

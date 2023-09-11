using Server_Shopping_cart.Configuration;
using Server_Shopping_cart.Services;
using Shopping_cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Server_Shopping_cart.Shopping_cart;

namespace Server_Shopping_cart.Container
{
    internal interface IContainer
    {
         List<Cart_Item_Struct> GetCartItems();

         List<CommandItem> GetCommands();

         UserRole GetUserRole();

        void SetUserRole(UserRole role);
    }
}

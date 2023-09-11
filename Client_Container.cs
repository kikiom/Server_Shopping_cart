using Server_Shopping_cart.Configuration;
using Server_Shopping_cart.Operations.Cart_Operations;
using Server_Shopping_cart.Operations.Comman_Operations;
using Server_Shopping_cart.Operations.Product_Operations;
using Server_Shopping_cart.Operations.Storage_Operations;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using Server_Shopping_cart.Shopping_cart;
using System.Collections.Generic;

namespace Server_Shopping_cart.Container
{
    class RoleList
    {
        public static UserRole[] All() { return new[] { UserRole.Admin, UserRole.Client, UserRole.Operator, UserRole.None }; }
        public static UserRole[] Admin_Client() { return new[] { UserRole.Admin, UserRole.Client }; }
        public static UserRole[] Admin_Only() { return new[] { UserRole.Admin }; }
        public static UserRole[] Client_Only() { return new[] { UserRole.Client }; }
        public static UserRole[] Operator_Only() { return new[] { UserRole.Operator }; }

    }
    public class Client_Container : IContainer
    {
        private List<CommandItem> _commands;
        private List<Cart_Item_Struct> _cart_items;
        private UserRole _curent_role;

        public Client_Container(IDatabase database)
        {
            _curent_role = UserRole.None;
            _cart_items = new List<Cart_Item_Struct>();
            _commands = new List<CommandItem>
            {
                new CommandItem(RoleList.All(), new Help()),
                new CommandItem(RoleList.All(), new Login()),
                new CommandItem(RoleList.Client_Only(), new Add_Cart_Item(database)),
                new CommandItem(RoleList.Client_Only(), new List_Cart_Items(database)),
                new CommandItem(RoleList.Client_Only(), new Remove_Cart_Item()),
                new CommandItem(RoleList.Client_Only(), new Update_Cart_Item(database)),
                new CommandItem(RoleList.Client_Only(), new Checkout(database)),
                new CommandItem(RoleList.Operator_Only(), new Update_Quantity(database)),
                new CommandItem(RoleList.Admin_Only(), new Add_Product(database)),
                new CommandItem(RoleList.Admin_Only(), new Edit_Product(database)),
                new CommandItem(RoleList.Admin_Client(), new Search_Product(database)),
                new CommandItem(RoleList.Admin_Only(), new Remove_Product(database)),
                new CommandItem(RoleList.Admin_Client(), new List_Product(database))
            };
        }

        public List<Cart_Item_Struct> GetCartItems()
        {
            return _cart_items;
        }

        public List<CommandItem> GetCommands()
        {
            return _commands;
        }

        public UserRole GetUserRole()
        {
            return _curent_role;
        }
        public void SetUserRole(UserRole role)
        {
            _curent_role = role;
        }
    }
}

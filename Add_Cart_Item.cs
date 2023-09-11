using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Services;
using Server_Shopping_cart.Shopping_cart;
using Server_Shopping_cart.Services.Parser.Command_Parser;
using Server_Shopping_cart.Product_DB;
using System;

namespace Server_Shopping_cart.Operations.Cart_Operations
{
    public class Add_Cart_Item : IOperation
    {
        private string _name = "add_cart_item";
        private IDatabase _database;

        public Add_Cart_Item( IDatabase database)
        {
            _database = database;
        }

        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new [] {typeof(int), typeof(int)});
            int product_id = arguments.AsNumber(0);
            int product_quantity = arguments.AsNumber(1);
            int id = container.GetCartItems().Count;
            foreach(Product_Struct product in _database.ListProducts())
            {
                if(product.GetId() == product_id)
                {
                    container.GetCartItems().Add(new Cart_Item_Struct(id, product_quantity, product_id));
                    return "fine";
                }
            }
            throw new Exception("not existing product");
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "add_cart_item ( id_product ; quantity ) - add a new item to the cart";
        }
    }
    
}

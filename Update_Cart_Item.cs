using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using Server_Shopping_cart.Shopping_cart;
using System;
using System.Linq;

namespace Server_Shopping_cart.Operations.Cart_Operations
{
    public class Update_Cart_Item : IOperation
    {
        private string _name = "update_cart_item";
        private IDatabase _database;

        public Update_Cart_Item(IDatabase database)
        {
            _database = database;
        }

        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new [] { typeof(int), typeof(int) } );
            int id_cat_item = arguments.AsNumber(0);
            int quantity = arguments.AsNumber(1);
            if (id_cat_item < 0)
            {
                throw new ArgumentException("invalid id");
            }
            if (quantity < 0)
            {
                throw new ArgumentException("invalid quantity");
            }
            foreach (Cart_Item_Struct cart_item in container.GetCartItems())
            {
                if(cart_item.GetId() == id_cat_item)
                {
                    if(_database.SearchProducts(cart_item.GetIdProduct()).GetQuantity() >= quantity)
                    {
                        cart_item.SetQuantity(quantity);
                        break;
                    }
                    else
                    {
                        return "not enough quantity";
                    }
                }
            }
            return "quantity is updated";
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "update_cart_item ( id_item ; quantity ) - updates the quantity of the item";
        }
    }
}

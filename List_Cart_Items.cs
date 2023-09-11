using Server_Shopping_cart.Container;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Shopping_cart;
using System;
using System.Linq;

namespace Server_Shopping_cart.Operations.Cart_Operations
{
    public class List_Cart_Items : IOperation
    {
        private string _name = "list_cart_items";
        private IDatabase _database;

        public List_Cart_Items(IDatabase database)
        {
            _database = database;
        }
        public string Exe(string args, Client_Container container)
        {
            string text = null;
            if (container.GetCartItems().Count() > 0)
            {
                foreach (Cart_Item_Struct item in container.GetCartItems())
                {

                    text = "ID : " + (item.GetId()).ToString();
                    foreach (Product_Struct product in _database.ListProducts())
                    {
                        if (item.GetIdProduct() == product.GetId())
                        {
                            text = text + "; Name : " + product.GetName();
                            text = text + "; Price : " + product.GetPrice();
                            break;
                        }
                    }

                    text = text + "; Quantity : " + item.GetQuantity();
                    text += "\n";
                }
            }
            else
            {
                text = "No products in the cart";
            }

            return text;
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "list_cart_items() - list all items in the cart";
        }
    }
}

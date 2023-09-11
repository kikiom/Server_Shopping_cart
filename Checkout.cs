using Server_Shopping_cart.Container;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Shopping_cart;
using System.Collections.Generic;

namespace Server_Shopping_cart.Operations.Cart_Operations
{
    public class Checkout : IOperation
    {
        private string _name = "checkout";
        private IDatabase _database;

        public Checkout(IDatabase database)
        {
            _database = database;
        }

        public string Exe(string args, Client_Container container)
        {
            if (container.GetCartItems().Count == 0)
            {
                return "no cart item";
            }
            string msg = "";
            double sum = 0;
            List<ProductQuantityChangedEventArgs> changes = new List<ProductQuantityChangedEventArgs>();
            foreach (Cart_Item_Struct cart_item in container.GetCartItems())
            {
                foreach (Product_Struct product in _database.ListProducts())
                {
                    if (cart_item.GetIdProduct() == product.GetId())
                    {
                        if (product.GetQuantity() >= cart_item.GetQuantity())
                        {
                            sum += product.GetQuantity() * product.GetPrice();
                            changes.Add(new ProductQuantityChangedEventArgs(cart_item.GetIdProduct(), product.GetQuantity() - cart_item.GetQuantity()));
                            break;
                        }
                        else
                        {
                            msg = msg + "pruduct " + product.GetName() + " does not have enough\n";
                        }
                    }
                }
            }
            container.GetCartItems().Clear();
            foreach (ProductQuantityChangedEventArgs productQuantityChanged in changes)
            {
                _database.UpdateProduct(productQuantityChanged.GetId(), productQuantityChanged.GetQuantity());
            }
            return msg + "\n finale sum is " + sum + "\n";

        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "checkout() - returns a sum for pay and clear cart";
        }
    }
}


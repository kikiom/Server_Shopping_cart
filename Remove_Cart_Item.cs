using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Services;
using System;

namespace Server_Shopping_cart.Operations.Cart_Operations
{
    public class Remove_Cart_Item : IOperation
    {
        private string _name = "remove_cart_item";


        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(int) });
            int id_cart_item = arguments.AsNumber(0);
            if (id_cart_item > 0)
            {
                for (int i = 0; i < container.GetCartItems().Count; i++)
                {
                    if (container.GetCartItems()[i].GetId() == id_cart_item)
                    {
                        container.GetCartItems().RemoveAt(i);
                        return "Item is deleted";
                    }
                }
            }
            throw new ArgumentException("invalid id");

        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "remove_cart_item ( id_item )- removes a cart item";
        }
    }
}

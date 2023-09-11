using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using System;

namespace Server_Shopping_cart.Operations.Product_Operations
{
    public class Remove_Product : IOperation
    {
        private string _name = "remove_product";
        private IDatabase _database;

        public Remove_Product(IDatabase database)
        {
            _database = database;
        }

        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(int) });
            int id = arguments.AsNumber(0);
            if (id < 0)
            {
                throw new ArgumentException("invalid id");

            }
            _database.DeleteProduct(id);
            return "product deleted \n";

        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "remove_product( id ) - removes a product";
        }
    }
}

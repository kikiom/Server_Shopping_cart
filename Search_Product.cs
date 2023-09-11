using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using System;
using System.Collections.Generic;

namespace Server_Shopping_cart.Operations.Product_Operations
{
    public class Search_Product : IOperation
    {
        private string _name = "search_product";
        private IDatabase _database;

        public Search_Product(IDatabase database)
        {
            _database = database;
        }
        public string GetName()
        {
            return _name;
        }
        public string print()
        {
            return "search_product( name ) - searching for a product";
        }

        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] {typeof(string)});
            string name = arguments.AsString(0);
            
            return _database.SearchProducts(name).ToString() + "\n";
            
        }
    }
}

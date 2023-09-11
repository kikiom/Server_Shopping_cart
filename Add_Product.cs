using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart.Operations.Product_Operations
{
    public class Add_Product : IOperation
    {
        private string _name = "add_product";
        private IDatabase _database;

        public Add_Product(IDatabase database)
        {
            _database = database;
        }

        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(string), typeof(float), typeof(int), typeof(string) });

            string name = arguments.AsString(0);
            float price = arguments.AsDecimal(1);
            int quantity = arguments.AsNumber(2);
            string description = arguments.AsString(3);

            _database.AddProduct(name, price, quantity, description);
            return "Product " + name + " added ";

        }
        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "add_product( name ; price ; quantity ; description) - adds a new product";
        }
    }

}

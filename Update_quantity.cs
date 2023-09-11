using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart.Operations.Storage_Operations
{
    internal class Update_Quantity : IOperation
    {
        private string _name = "update_quantity";
        private IDatabase _database_services;

        public Update_Quantity(IDatabase database_services)
        {
            _database_services = database_services;
        }
        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(int), typeof(int) });

            string msg = "";
            int quantity = arguments.AsNumber(1);
            if (quantity >= 0)
            {
                _database_services.UpdateProduct(arguments.AsNumber(0), quantity);
                msg = "Product is editted";

            }
            else if (quantity == 0)
            {
                _database_services.DeleteProduct(arguments.AsNumber(0));
            }
            else
            {
                msg = "Wrong input quantity";

            }

            return msg;

        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "update_quantity( id; quantity) - updates the product quantity";
        }
    }
}

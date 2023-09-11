using Server_Shopping_cart.Container;
using Server_Shopping_cart.Product_DB;

namespace Server_Shopping_cart.Operations.Product_Operations
{
    public class List_Product : IOperation
    {
        private string _name = "list_products";
        private IDatabase _database;

        public List_Product(IDatabase database)
        {
            _database = database;
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "list_products() - shows all products";
        }

        public string Exe(string args, Client_Container container)
        {
            string msg = "";

            if (_database.ListProducts() != null)
            {
                foreach (Product_Struct product in _database.ListProducts())
                {
                    msg = msg + product.ToString() + "\n";
                }
            }
            else
            {
                msg = "No products";
            }

            return msg;
        }
    }
}

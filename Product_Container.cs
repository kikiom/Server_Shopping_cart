using System.Collections.Generic;
using System.ComponentModel;

namespace Server_Shopping_cart.Product_DB
{
    public class Product_Container
    {
        private IDatabase _database;
        public Product_Container()
        {
            _database = new Database_Services();
            _database.Init();
        }
        public IDatabase Get_Product_database()
        {
            return _database;
        }

    }
}

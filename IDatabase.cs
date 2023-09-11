using Server_Shopping_cart.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Shopping_cart.Product_DB
{
    public interface IDatabase
    {
        void AddProduct(string name, float price, int quantity, string descripton);

        void Cleanup();

        void DeleteProduct(int id);

        void Init();

        List<Product_Struct> ListProducts();

        List<Product_Struct> SearchProducts(string name);

        Product_Struct SearchProducts(int id);

        void UpdateProduct(int id, string name, bool is_name);

        void UpdateProduct(int id, int quantity);

        void UpdateProduct(int id, float price);

        ProductEventManager GetProductEventManager();
    }
}

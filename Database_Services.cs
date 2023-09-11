using Server_Shopping_cart.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Server_Shopping_cart.Product_DB
{
    public class Database_Services : IDatabase
    {
        private List<Product_Struct> _products;
        private object _product_lock;
        public ProductEventManager _eventManager;

        private string _filename = "save.txt";

        public void AddProduct(string name, float price, int quantity, string descripton)
        {
            lock (_product_lock)
            {
                int last_id;
                if (_products.Any())
                {
                    last_id = _products.Last().GetId();
                }
                else
                {
                    last_id = 0;
                }
                
                Product_Struct product = new Product_Struct(last_id + 1, quantity, price, name, descripton);
                _products.Add(product);

            }
        }

        public void Cleanup()
        {
            lock (_product_lock)
            {
                using (StreamWriter writer = new StreamWriter(_filename))
                {

                    foreach (Product_Struct item in _products)
                    {
                        writer.WriteLine(item.ToSave());
                    }

                }
            }
        }

        public void DeleteProduct(int id)
        {
            lock (_product_lock)
            {
                Product_Struct delete_product = null;
                foreach (Product_Struct item in _products)
                {
                    if (item.GetId() == id)
                    {
                        delete_product = item;
                        break;
                    }
                }
                if (delete_product != null)
                    _products.Remove(delete_product);
            }
        }

        public void Init()
        {
            _eventManager = new ProductEventManager();
            _product_lock = new object();
            _products = new List<Product_Struct>();
            lock (_product_lock)
            {
                if (File.Exists(_filename))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(_filename))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            char[] separator = { ';' };
                            string[] sub = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            //sub0 = id     sub1 = name     sub2 = price    sub3 = quantity     sub4 = description
                            _products.Add(new Product_Struct(int.Parse(sub[0].Trim()), int.Parse(sub[3].Trim()), float.Parse(sub[2].Trim()), sub[1].Trim(), sub[4].Trim()));
                        }
                    }
                }
            }


        }

        public List<Product_Struct> ListProducts()
        {
            List<Product_Struct> products = new List<Product_Struct>();
            products.AddRange(_products);
            return products;
        }

        public List<Product_Struct> SearchProducts(string name)
        {
            lock (_product_lock)
            {
                List<Product_Struct> products = new List<Product_Struct>();
                foreach (Product_Struct item in _products)
                {
                    if (item.GetName() == name)
                    {
                        products.Add(item);
                    }
                }
                return products;
            }
        }

        public Product_Struct SearchProducts(int id)
        {
            lock (_product_lock)
            {
                Product_Struct product = new Product_Struct();
                foreach (Product_Struct item in _products)
                {
                    if (item.GetId() == id)
                    {
                        return product;
                    }
                }
                throw new Exception("no product with this id");
                
            }
        }

        public void UpdateProduct(int id, string name, bool is_name)
        {
            lock (_product_lock)
            {
                foreach (Product_Struct item in _products)
                {
                    if (item.GetId() == id)
                    {
                        if (is_name)
                        {
                            item.SetName(name);
                        }
                        else
                        {
                            item.SetDescription(name);
                        }
                    }
                }
            }
        }

        public void UpdateProduct(int id, int quantity)
        {
            lock (_product_lock)
            {
                foreach (Product_Struct item in _products)
                {
                    if (item.GetId() == id)
                    {

                        item.SetQuantity(quantity);

                        // Trigger the quantity changed event through the event manager
                        _eventManager.OnProductQuantityChanged(new ProductQuantityChangedEventArgs(id, quantity));

                    }
                }
            }
        }

        public void UpdateProduct(int id, float price)
        {
            lock (_product_lock)
            {
                foreach (Product_Struct item in _products)
                {
                    if (item.GetId() == id)
                    {
                        item.SetPrice(price);
                    }
                }
            }
        }
        public ProductEventManager GetProductEventManager()
        {
            return _eventManager;
        }
    }
}

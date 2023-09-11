using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using System;

namespace Server_Shopping_cart.Operations.Product_Operations
{
    public class Edit_Product : IOperation
    {
        private string _name = "edit_product";
        private IDatabase _database_services;

        public Edit_Product(IDatabase database_services)
        {
            _database_services = database_services;
        }

        public string GetName()
        {
            return _name;
        }


        public string print()
        {
            return "edit_product( id ; feild ; new data) - eidt the specified product";
        }


        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments;
            try
            {
                arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(int), typeof(string), typeof(string) });

            }
            catch (Exception ex)
            {

                arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(int), typeof(string), typeof(float) });

            }

            string msg = "";

            switch (arguments.AsString(1))
            {
                case "name":
                    _database_services.UpdateProduct(arguments.AsNumber(0), arguments.AsString(2), true);
                    msg = "Product is editted";
                    break;

                case "price":
                    float price = arguments.AsDecimal(2);
                    if (price >= 0)
                    {
                        _database_services.UpdateProduct(arguments.AsNumber(0), price);
                        msg = "Product is editted";
                    }
                    else
                    {
                        msg = "Wrong input price";
                    }
                    break;

                case "quantity":

                    int quantity = arguments.AsNumber(2);
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
                    break;

                case "description":

                    _database_services.UpdateProduct(arguments.AsNumber(0), arguments.AsString(2), false);
                    msg = "Product is editted";
                    break;

                default:

                    msg = "incorect field name";

                    break;

            }
            return msg;
        }
    }
}

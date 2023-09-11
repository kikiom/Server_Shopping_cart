namespace Server_Shopping_cart.Product_DB
{
    public class Product_Struct
    {
        private string _name;
        private string _description;
        private float _price;
        private int _quantity;
        private readonly int _id;

        public Product_Struct(int id, int quantity, float price, string name, string description)
        {
            _id = id;
            _quantity = quantity;
            _name = name;
            _price = price;
            _description = description;

        }

        public Product_Struct()
        {
        }

        public float GetPrice()
        {
            return _price;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public string GetDescription()
        {
            return _description;
        }
        public int GetQuantity()
        {
            return _quantity;
        }
        public void SetPrice(float price)
        {
            _price = price;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public void SetDescription(string description)
        {
            _description = description;
        }
        public void SetQuantity(int quantity)
        {
            _quantity = quantity;
        }
        public override string ToString()
        {
            return "ID: "+ _id + "; Name: " + _name + "; Price: " + _price + "; Quantity: " + _quantity + "; Description: " + _description;
        }
        public string ToSave()
        {
            return  _id + ";" + _name + ";" + _price + ";" + _quantity + ";" + _description;
        }
    }
}

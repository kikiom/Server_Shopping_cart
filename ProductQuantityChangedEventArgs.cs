namespace Server_Shopping_cart.Product_DB
{
    public class ProductQuantityChangedEventArgs
    {
        private int _id;
        private int _quantity;

        public ProductQuantityChangedEventArgs(int id, int quantity)
        {
            _id = id;
            _quantity = quantity;
        }
        
        public int GetId()
        {
            return _id;
        }

        public int GetQuantity()
        {
            return _quantity;
        }
    }
}
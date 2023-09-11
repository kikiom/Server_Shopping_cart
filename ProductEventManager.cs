using System;
using Server_Shopping_cart.Product_DB;

namespace Server_Shopping_cart.Notifications
{
    public class ProductEventManager
    {
        public event EventHandler<ProductQuantityChangedEventArgs> ProductQuantityChanged;

        public virtual void OnProductQuantityChanged(ProductQuantityChangedEventArgs e)
        {
            ProductQuantityChanged?.Invoke(this, e);
        }
    }
}

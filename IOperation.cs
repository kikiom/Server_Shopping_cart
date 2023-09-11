using Server_Shopping_cart.Container;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart
{
    public interface IOperation
    {
        string print();
        string GetName();
        string Exe(string args, Client_Container container);
    }
}

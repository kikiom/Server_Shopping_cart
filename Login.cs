using Server_Shopping_cart.Configuration;
using Server_Shopping_cart.Container;
using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart.Operations.Comman_Operations
{
    public class Login : IOperation
    {
        private string _name = "login";
        public string Exe(string args, Client_Container container)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(UserRole) });

            var role = arguments.AsRole(0);

            container.SetUserRole(role);
            return "role set to " + role.ToString();
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "login ( type of user )- logs you in a account";
        }
    }
}

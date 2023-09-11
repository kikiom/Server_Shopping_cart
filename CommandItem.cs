using Server_Shopping_cart.Configuration;
using System.Collections.Generic;

namespace Server_Shopping_cart.Services
{
    public class CommandItem
    {
        public IOperation Handler { get; private set; }
        public List<UserRole> Roles { get; private set; }

        public CommandItem(UserRole[] roles, IOperation handler)
        {
            Handler = handler;
            Roles = new List<UserRole> ();

            foreach (var role in roles)
            {
                Roles.Add(role);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using Server_Shopping_cart;
using Server_Shopping_cart.Configuration;
using Server_Shopping_cart.Container;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart.Operations.Comman_Operations
{
    public class Help : IOperation
    {
        private string _name = "help";

        public string Exe(string args, Client_Container container)
        {
            string msg = "";
            foreach (CommandItem command in container.GetCommands())
            {
                foreach(UserRole role in command.Roles)
                {
                    if(role == container.GetUserRole())
                    {
                        msg += command.Handler.print();
                        msg += "\n";
                    }
                }
                
            }
            return msg;
        }

        public string GetName()
        {
            return _name;
        }

        public string print()
        {
            return "help for current options";
        }
    }
}

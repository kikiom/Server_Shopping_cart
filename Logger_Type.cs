using Server_Shopping_cart.Operations.Util;
using Server_Shopping_cart.Services;

namespace Server_Shopping_cart.Logger
{
    public class Logger_Type
    {
        private string _name = "logger_type";
        public void Exe(string args)
        {
            Command_Arguments arguments = CommandUtil.ValidateArgs(_name, args, new[] { typeof(string) });

            Logger.Set_Type_MSG(arguments.AsString(0));
        }

        public bool CheckType(string type)
        {
            return true;
        }

        public string GetName()
        {
            return _name;

        }

        public string print()
        {
            return "logger_type ( type ) - set the type of msg level, the default is error level";
        }
    }
}

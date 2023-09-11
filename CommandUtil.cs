using Server_Shopping_cart.Services;
using Server_Shopping_cart.Services.Parser.Command_Parser;
using System;
using System.Linq;

namespace Server_Shopping_cart.Operations.Util
{
    public class CommandUtil
    {
        public static Command_Arguments ValidateArgs(string name, string arguments, Type[] types)
        {
            //need to parse
            Command_Arguments args = Command_Parser.ParseArguments(arguments);
            if (args.Count != types.Length)
            {
                throw new Exception($"Invalid number of arguments passed to command \"{name}\"");
            }

            for (int i = 0; i < types.Length; i++)
            {
                if (args.GetAt(i).GetType() == typeof(float)
                    && (types[i] == typeof(float) || types[i] == typeof(int)))
                {
                    continue;
                }
                if (types[i] != args.GetAt(i).GetType())
                {
                    throw new Exception($"Invalid type of argument {i} passed to command \"{name}\"");
                }
            }
            return args;
        }
    }
}

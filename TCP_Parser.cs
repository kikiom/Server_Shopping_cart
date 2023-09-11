using Server_Shopping_cart.Configuration;
using System;
using System.Collections.Generic;

namespace Server_Shopping_cart.Services.Parser.Protocol_Parser
{
    public class TCP_Parser
    {
        public static List<object> Parse(string args)
        {
            int pos = 0;
            List<object> parsed_command = new List<object>();

            while (pos < args.Length)
            {
                SkipR(args, ref pos);
                bool isValidType = ParseCommand(args, parsed_command, ref pos);

                if (!isValidType)
                {
                    throw new Exception($"Invalid argument type at position: {parsed_command.Count + 1}");
                }

                SkipWhitespace(args, ref pos);
                SkipR(args, ref pos);
            }
            return parsed_command;
        }

        private static void SkipWhitespace(string args, ref int pos)
        {
            for (; pos < args.Length; pos++)
            {
                if (!char.IsWhiteSpace(args[pos]))
                {
                    break;
                }
            }
        }
        private static void SkipR(string args, ref int pos)
        {
            for (; pos < args.Length; pos++)
            {
                
                if (!(args[pos] =='\r'))
                {
                    break;
                }
            }
        }

        private static bool ParseCommand(string args, List<object> parsedArguments, ref int pos)
        {
            if (args[pos] != '\n')
            {
                int start = pos;
                pos++;

                for (; pos < args.Length; pos++)
                {
                    if (args[pos] == '\n' )
                    {
                        string thisArgs = args.Substring(start , pos - start);

                        parsedArguments.Add(thisArgs);

                        pos++;
                        return true;
                    }
                }

                throw new Exception($"Invalid string argument at position: {parsedArguments.Count + 1}");
            }

            return false;
        }


    }
}

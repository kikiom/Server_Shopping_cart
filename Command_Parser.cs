using Server_Shopping_cart.Configuration;
using System;
using System.Collections.Generic;

namespace Server_Shopping_cart.Services.Parser.Command_Parser
{
    internal class Command_Parser
    {
        public static (string, string) Parse(string cmd)
        {
            string[] res = cmd.Trim().Split('(');

            if (res.Length == 1)
            {
                return (res[0].Trim(), "");
            }

            if (!res[1].EndsWith(")"))
            {
                throw new Exception("Invalid command syntax: missing closing bracket.");
            }

            return (res[0].Trim(), res[1].Substring(0, res[1].Length - 1).Trim());
        }

        public static Command_Arguments ParseArguments(string args)
        {
            int pos = 0;
            List<object> parsedArguments = new List<object>();

            while (pos < args.Length)
            {
                bool isValidType = ParseStringArg(args, parsedArguments, ref pos)
                    || ParseRoleArg(args, parsedArguments, ref pos)
                    || ParseDecimalArg(args, parsedArguments, ref pos);

                if (!isValidType)
                {
                    throw new Exception($"Invalid argument type at position: {parsedArguments.Count + 1}");
                }

                SkipWhitespace(args, ref pos);

                if (pos < args.Length && args[pos] != ';')
                {
                    throw new Exception($"Invalid characters after argument at position: {parsedArguments.Count + 1}");
                }

                pos++;

                SkipWhitespace(args, ref pos);
            }

            return new Command_Arguments(parsedArguments);
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

        private static bool ParseStringArg(string args, List<object> parsedArguments, ref int pos)
        {
            if (args[pos] == '"')
            {
                int start = pos;
                pos++;

                for (; pos < args.Length; pos++)
                {
                    if (args[pos] == '"' && args[pos - 1] != '\\')
                    {
                        string thisArgs = args.Substring(start + 1, pos - start - 1);

                        parsedArguments.Add(thisArgs);

                        pos++;
                        return true;
                    }
                }

                throw new Exception($"Invalid string argument at position: {parsedArguments.Count + 1}");
            }

            return false;
        }

        private static bool ParseRoleArg(string args, List<object> parsedArguments, ref int pos)
        {
            string test = args.Substring(pos);

            foreach (var enumName in Enum.GetNames(typeof(UserRole)))
            {
                if (test.StartsWith(enumName))
                {
                    pos += enumName.Length;
                    parsedArguments.Add(Enum.Parse(typeof(UserRole), enumName));

                    return true;
                }
            }

            return false;
        }

        private static bool ParseDecimalArg(string args, List<object> parsedArguments, ref int pos)
        {
            int start = pos;

            for (; pos < args.Length; pos++)
            {
                if (!char.IsDigit(args[pos]) && args[pos] != '-' && args[pos] != '.')
                {
                    break;
                }
            }

            if (pos == start)
            {
                return false;
            }

            string strDec = args.Substring(start, pos - start);

            if (!float.TryParse(strDec, out float decArg))
            {
                throw new Exception($"Invalid decimal argument at position: {parsedArguments.Count + 1}");
            }

            parsedArguments.Add((float)decArg);
            return true;
        }
    }
}

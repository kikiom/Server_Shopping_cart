using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_cart
{
    public class Read
    {
        public void ReadFromTerminal(ref string command, ref string data)
        {
            string input = Console.ReadLine();
            char[] separators = new char[] { '(', ')' };
            if (input != null)
            {
                string[] subs = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (subs.Length == 2)
                {
                    command = subs[0].Trim();
                    if (subs.Length > 1)
                    {
                        data = subs[1];
                    }
                    else
                    {
                        data = null;
                    }
                }
                if (subs.Length == 1)
                {
                    command = subs[0].Trim();
                }
                if (subs.Length == 0)
                {
                    Console.WriteLine("No input");
                }

            }

        }
    }
}

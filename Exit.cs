using System;

namespace Server_Shopping_cart.Operation.Comman_Operations
{
    public class Exit 
    {
        private string _name = "exit";

        public void Exe(string args)
        {
            Console.WriteLine("bye");
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
            return "exit () - exits the program";
        }
    }
}

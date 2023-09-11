using Server_Shopping_cart.Container;
using System;
using System.IO;

namespace Server_Shopping_cart.Logger
{
    enum Type_MSG
    {
        erorr,
        warn,
        info,
        debug
    }
    public static class Logger
    {
        private static int _type_msg = 0;
        public static void Set_Type_MSG(string type_msg)
        {
            switch (type_msg)
            {
                case "error":
                    _type_msg = 0;
                    break;
                case "warn":
                    _type_msg = 1;
                    break;
                case "info":
                    _type_msg = 2;
                    break;
                case "debug":
                    _type_msg = 3;
                    break;
                default:
                    _type_msg = 0;
                    break;
            }

        }
        public static void Log(Client_Container client_container, string type_msg, string message)
        {
            int this_type_msg;
            switch (type_msg)
            {
                case "error":
                    this_type_msg = 0;
                    break;
                case "warn":
                    this_type_msg = 1;
                    break;
                case "info":
                    this_type_msg = 2;

                    break;
                case "debug":
                    this_type_msg = 3;
                    break;
                default:
                    this_type_msg = 0;
                    break;
            }
            if (this_type_msg <= _type_msg)
            {
                Save(client_container, type_msg, message);
            }
        }

        public static void Save(Client_Container client_container, string type_msg, string message)
        {

            string logFilePath = "example.log"; // Replace this with your actual log file path

            // Information to append to the log file
            string logInformation = DateTime.Now.ToString() + " / " + type_msg + " / " + client_container.GetUserRole().ToString() + " / " + message + "\n";

            try
            {
                // Append the information to the log file
                File.AppendAllText(logFilePath, logInformation);


            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static new int GetType()
        {
            return _type_msg;
        }
    }
}

using Server_Shopping_cart.Configuration;
using Server_Shopping_cart.Container;
using Server_Shopping_cart.Notifications;
using Server_Shopping_cart.Product_DB;
using Server_Shopping_cart.Services;
using Server_Shopping_cart.Services.Parser.Command_Parser;
using Server_Shopping_cart.Services.Parser.Protocol_Parser;
using Server_Shopping_cart.Shopping_cart;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server_Shopping_cart.Client
{
    public class Client_Handler
    {
        private readonly TcpClient _client_obj;
        //private readonly Server_Handler _server_handler;
        Client_Container _container;
        ProductEventManager _event_manager;
        NetworkStream _stream;
        Thread _my_thread;
        public Client_Handler(object clientObj, IDatabase database)
        {
            _client_obj = (TcpClient)clientObj;
            //_server_handler = server_handler;
            _container = new Client_Container(database);
            _event_manager = database.GetProductEventManager();
            _stream = _client_obj.GetStream();
        }

        public void Exe()
        {


            // Subscribe to the ProductQuantityChanged event
            _event_manager.ProductQuantityChanged += EventMetothed;



            byte[] buffer = new byte[1024];
            int bytesRead;


            try
            {
                while (!_stream.DataAvailable)
                {
                    try
                    {
                        if((bytesRead = _stream.Read(buffer, 0, buffer.Length)) == 0)
                        {
                            Close();
                            break;
                        }
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Array.Clear(buffer, 0, buffer.Length);
                        Console.WriteLine($"Received: {message}");
                        List<object> commands = TCP_Parser.Parse(message);
                        if (commands == null)
                        {
                            break;
                        }
                        foreach (object command in commands)
                        {
                            int command_found = 0;
                            string commandmsg;
                            string command_name, command_args;
                            (command_name, command_args) = Command_Parser.Parse(command.ToString());

                            foreach (CommandItem operation in _container.GetCommands())
                            {
                                if (operation.Handler.GetName() == command_name)
                                {
                                    command_found += 1;
                                    foreach (UserRole role in operation.Roles)
                                    {
                                        if (role == _container.GetUserRole())
                                        {
                                            command_found += 2;
                                            commandmsg = operation.Handler.Exe(command_args, _container);
                                            Console.WriteLine(commandmsg);
                                            SendMessage(commandmsg, _client_obj);
                                            break;
                                        }
                                    }

                                }

                            }
                            switch (command_found)
                            {
                                case 0:
                                    SendMessage("No command with this name", _client_obj);
                                    break;
                                case 1:
                                    SendMessage("Not right access level", _client_obj);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("client" + _client_obj.Client.ToString());
                        SendMessage(e.Message, _client_obj);
                        Console.WriteLine(e.Message);
                    }


                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Close();
            }


        }

        public void SendMessage(string message, TcpClient senderClient)
        {
            lock (this)
            {
                NetworkStream stream = senderClient.GetStream();
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
                stream.Flush();

            }
        }

        public void EventMetothed(object sender, ProductQuantityChangedEventArgs e)
        {
            bool found = false;
            foreach (Cart_Item_Struct cart_item in _container.GetCartItems())
            {
                if (e.GetId() == cart_item.GetIdProduct())
                {
                    if (e.GetQuantity() < cart_item.GetQuantity())
                    {
                        found = true;
                    }
                }
            }
            if (found)
            {
                string msg = $"Product with ID {e.GetId()} quantity changed to {e.GetQuantity()} , so change it before checkout.";
                SendMessage(msg, _client_obj);
                Console.WriteLine(msg);
            }
        }
        public void Close()
        {
            lock (this)
            {
                Console.WriteLine("Client disconnected!");
                _client_obj.Close();
                _stream.Close();
                _my_thread.Join();                
            }

        }
        
        public void AddThread(Thread thread)
        {
            _my_thread = thread;
        }
    }
}

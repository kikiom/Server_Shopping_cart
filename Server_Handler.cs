using Server_Shopping_cart.Client;
using Server_Shopping_cart.Notifications;
using Server_Shopping_cart.Product_DB;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Shopping_cart.TCP_Server
{
    public class Server_Handler
    {
        private TcpListener _listener;
        public List<TcpClient> _connectedClients;
        private readonly int _port = 12345;
        public object _clientLock;
        CancellationTokenSource _cancellationTokenSource;
        private bool _run;
        public Product_Container _product_container;
        public event EventHandler<TcpClientEventArgs> AcceptTcpClientEvent;

        public Server_Handler()
        {
            _run = true;
            _clientLock = new object();
            _connectedClients = new List<TcpClient>();
            _listener = new TcpListener(IPAddress.Any, _port);
            _cancellationTokenSource = new CancellationTokenSource();
            _product_container = new Product_Container();
        }

        public void Execute()
        {
            Console.WriteLine("Server started, waiting for clients...");
            _listener.Start();


            // To start the server
            System.Threading.Tasks.Task.Run(() => HandleAcceptTCP(_cancellationTokenSource.Token));




            while (_run)
            {
                string message = Console.ReadLine();

                if (message == "exit")
                {
                    Shutdown();
                    break;
                }

                // Broadcast the message to all clients
                BroadcastMsg("Server: " + message);
            }
        }


        public void Shutdown()
        {
            // To shutdown the server
            _cancellationTokenSource.Cancel();
            Console.WriteLine("Shutting down the server...");
            BroadcastMsg("Server is shutting down");
            
            lock (_clientLock)
            {
                foreach (TcpClient client in _connectedClients)
                {
                    try
                    {
                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error closing client connection: {ex.Message}");
                    }
                }
                _connectedClients.Clear();
            }
            
            _product_container.Get_Product_database().Cleanup();
            _listener.Stop();
            _run = false;
        }

        private Task HandleAcceptTCP(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_listener.Pending())
                    {
                        TcpClient client = _listener.AcceptTcpClient();
                        Console.WriteLine("Client connected!");
                        OnAcceptTcpClient(new TcpClientEventArgs(client));

                        lock (_clientLock)
                        {
                            _connectedClients.Add(client);
                        }
                        Client_Handler client_Handler = new Client_Handler(client, _product_container.Get_Product_database());
                        Thread clientThread = new Thread(client_Handler.Exe);
                        client_Handler.AddThread(clientThread);
                        clientThread.Start();
                    }

                }
                catch (SocketException ex)
                {
                    // Handle socket exceptions if needed
                    Console.WriteLine($"SocketException in HandleAcceptTCP: {ex.Message}");
                }
                Thread.Sleep(10);
            }
            return Task.CompletedTask;
        }

        protected virtual void OnAcceptTcpClient(TcpClientEventArgs e)
        {
            AcceptTcpClientEvent?.Invoke(this, e);
        }

        void BroadcastMsg(string msg)
        {
            byte[] shutdownMessageBytes = Encoding.ASCII.GetBytes(msg);
            lock (_clientLock)
            {
                foreach (TcpClient client in _connectedClients)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(shutdownMessageBytes, 0, shutdownMessageBytes.Length);
                        stream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending shutdown message to client: {ex.Message}");
                    }
                }
            }
        }
    }
}

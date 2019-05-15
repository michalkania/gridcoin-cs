using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Gridcoin;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            RunExample().Wait();
        }

        private static async Task RunExample()
        {
            // Users running the GUI must set server=1 in gridcoinresearch.conf
            // to enable the RPC server.
            var client = new GridcoinClient(
                "127.0.0.1",
                "kozak", // rpcuser from gridcoinresearch.conf
                "rhgwck");   // rpcpassword from gridcoinresearch.conf
            string command = "";
            while (!command.Equals("exit"))
            {
                try
                {
                    //ServerInfo info = await client.GetInfo();

                    //Console.Write("Version: ");
                    //Console.WriteLine(info.Version);
                    //Console.Write("Balance: ");
                    //Console.WriteLine(info.Balance);

                    Console.Write("Command: ");
                    command = Console.ReadLine();
                    await client.ExecuteCommand(command);
                    //long height = await client.GetBlockCount();

                    //Console.Write("Height:  ");
                    //Console.WriteLine(height);
                }
                catch (IOException e)
                {
                    Console.Write("Network error: ");
                    Console.WriteLine(e.Message);
                }
                catch (HttpRequestException e)
                {
                    Console.Write("Request error: ");
                    Console.WriteLine(e.Message);
                }
                catch (SerializationException e)
                {
                    Console.Write("Serialization format error: ");
                    Console.WriteLine(e.Message);
                }
                catch (RpcException e)
                {
                    Console.Write("RPC error: Code: ");
                    Console.WriteLine(e.Code);
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.Write("Unknown error: ");
                    Console.WriteLine(e);
                }
            }
        }
    }
}

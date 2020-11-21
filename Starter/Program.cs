using System;
using System.Security.Cryptography.X509Certificates;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceClient.Protos;

namespace Starter
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var clientRental=new Clients.ClientsClient(channel);

            var response = await clientRental.GetClientByIdAsync(new ClientRequestID() { Id = "1" });

            Console.WriteLine("By Id: " + response.ForName);

            var response2 = await clientRental.GetClientByIdAsync(new ClientRequestID() { Id = "1" });

            Console.WriteLine("By Name: " + response2.ForName);
            Console.ReadLine();
        }
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceCar.Protos;
using GrpcServiceClient.Protos;
using Microsoft.AspNetCore.Identity;
using Shared.helpers;
using Shared.model;

namespace Starter
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            //var clientRental=new Clients.ClientsClient(channel);
            //using var callPush = clientRental.AddClientAsync(new ClientDTO
            //{
            //    ID = 5,
            //    BirthDate = new Timestamp(),
            //    City = "gent",
            //    Country = "Belgium",
            //    Drivinglicence = "sdfdsf",
            //    Email = "bfsfsdf@mil.be",
            //    ForName = "bfd",
            //    IdCarNbr = "12",
            //    Name = "benoit",
            //    Nbr = "456",
            //    Street = "dfdsf",
            //    Tel = "0475981572",
            //    Zip = "9000",
            //});
            //var response = await clientRental.GetClientByIdAsync(new ClientRequestID() { Id = 5 });

            //Console.WriteLine("By Id: " + response.ForName);

            //var response2 = await clientRental.GetClientByNameAsync(new ClientRequestName() { Name = "benoit" });

            //Console.WriteLine("By Name: " + response2.ForName);


            //using var call = clientRental.GetAllClient(new Empty());

            //await foreach (var responseStream in call.ResponseStream.ReadAllAsync())
            //{
            //    Console.WriteLine("Greeting: " + responseStream.Name);
            //    // "Greeting: Hello World" is written multiple times
            //}
            var channel2 = GrpcChannel.ForAddress("https://localhost:5001");
            var clcar = new Cars.CarsClient(channel2);
            using var callPushCar = clcar.AddCarAsync(new CarDTO()
            {
                InCirculationDate = ConverterTime.UtcConverter(DateTime.Now),
                Id = 1,
                Km = 10,
                LastMaintenace = ConverterTime.UtcConverter(DateTime.Now),
                NextMaintenace = ConverterTime.UtcConverter(DateTime.Now),
                Operational = true,
                PlateNbr = "fsdfdsfdsf",
                Remarks = "dsfdsdsf",
                TypeCar = CarDTO.Types.TyepeOfCar.Coupe

            });

            using var call = clcar.GetAllCar(new Empty());

            await foreach (var responseStream in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("Greeting: " + responseStream.PlateNbr);
                // "Greeting: Hello World" is written multiple times
            }

            var response2 = await clcar.GetCaryIdAsync(new CarRequestID() {Id = 1});

            Console.WriteLine("By Id: " + response2);

            var response3 = await clcar.GetCarByplateAsync(new CarRequestPlate(){Name="fsdfdsfdsf"});

            Console.WriteLine("By PlateNbr: " + response3.PlateNbr);


            var response4 = await clcar.addTimeSlotAsync(new TimeSlotCarId()
            {
                IdCar = response3.Id,
                Timeslot = new TimeSLot()
                    {StartSLot = ConverterTime.UtcConverter(DateTime.Now), EndSlot = ConverterTime.UtcConverter(DateTime.Now.AddDays(1))}
            });

            Console.WriteLine("By PlateNbr: " + response4);


            Console.ReadLine();
        }
    }
}

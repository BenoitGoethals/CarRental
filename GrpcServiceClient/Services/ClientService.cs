using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceClient.Protos;
using Microsoft.Extensions.Logging;

namespace GrpcServiceClient.Services
{
    public class ClientService : Clients.ClientsBase
    {
        private readonly ILogger<ClientService> _logger;
        public ClientService(ILogger<ClientService> logger)
        {
            _logger = logger;
        }

        public override Task<ClientReply> GetClientById(ClientRequestID request, ServerCallContext context)
        {
             return Task.FromResult(new ClientReply
             {
               ID= request.Id,
               BirthDate=new Timestamp(),
               City="gent",
               Country="Belgium",
               Drivinglicence="sdfdsf",
               Email="bfsfsdf@mil.be",
               ForName="bfd",
               IdCarNbr="4565465",
               Name="benoit",
               Nbr="456",
               Street="dfdsf",
               Tel="0475981572",
               Zip="9000",
            });
        }

        public override Task<ClientReply> GetClientByName(ClientRequestName request, ServerCallContext context)
        {
            return Task.FromResult(new ClientReply
            {
                ID = "454",
                BirthDate = new Timestamp(),
                City = "gent",
                Country = "Belgium",
                Drivinglicence = "sdfdsf",
                Email = "bfsfsdf@mil.be",
                ForName = "bfd",
                IdCarNbr = request.IdCard,
                Name = "benoit",
                Nbr = "456",
                Street = "dfdsf",
                Tel = "0475981572",
                Zip = "9000",
            });
        }
    }
}

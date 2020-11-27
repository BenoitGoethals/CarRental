using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceClient.data;
using GrpcServiceClient.Migrations;
using GrpcServiceClient.Protos;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.model;
using Shared.validator;

namespace GrpcServiceClient.Services
{
    public class ClientService : Clients.ClientsBase
    {
        private readonly ILogger<ClientService> _logger;
        private readonly ClientDbContext _clientDbContext;

        public ClientService(ILogger<ClientService> logger, ClientDbContext clientDbContext)
        {
            _logger = logger;
            _clientDbContext = clientDbContext;
        }

     

        public override async Task GetAllClient(Empty request, IServerStreamWriter<ClientDTO> responseStream, ServerCallContext context)
        {
            await _clientDbContext.Clients.ForEachAsync(c =>
            {
                responseStream.WriteAsync(new ClientDTO()
             {
              
                 BirthDate = UtcConverter(c.BirthDate),
                 City =c.City,
                 Country = c.Country,
                 Drivinglicence=c.DrivingLicence,
                 Email=c.Email,
                 ForName=c.ForName,
                 IdCarNbr=c.IdCarNbr,
                 Name=c.Name,
                 ID=c.Id,
                 Tel=c.Tel,
                 Zip=c.Zip,
                 Street = c.Street,
                 Nbr=c.Nbr
             });


            });

            
        }

        private Timestamp UtcConverter(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.SpecifyKind(date.Date, DateTimeKind.Unspecified), "Eastern Standard Time", "UTC").ToTimestamp();

        }

        public override async Task<ClientDTO> GetClientById(ClientRequestID request, ServerCallContext context)
        {

            Client c = await _clientDbContext.Clients.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));
            if (c != null)
            {
                ClientDTO cl = new ClientDTO()
                {
                    BirthDate = UtcConverter(c.BirthDate),
                    City = c.City,
                    Country = c.Country,
                    Drivinglicence = c.DrivingLicence,
                    Email = c.Email,
                    ForName = c.ForName,
                    IdCarNbr = c.IdCarNbr,
                    Name = c.Name,
                    ID = c.Id,
                    Tel = c.Tel,
                    Zip = c.Zip,
                    Street = c.Street,
                    Nbr = c.Nbr

                };
                return cl;
            }
            return new ClientDTO();
        

        }

        public override async Task<ClientDTO> GetClientByName(ClientRequestName request, ServerCallContext context)
        {

            Client c = await _clientDbContext.Clients.FirstOrDefaultAsync(t => t.Name.Equals(request.Name));
            if (c != null)
            {
                ClientDTO cl = new ClientDTO()
                {
                    BirthDate = UtcConverter(c.BirthDate),
                    City = c.City,
                    Country = c.Country,
                    Drivinglicence = c.DrivingLicence,
                    Email = c.Email,
                    ForName = c.ForName,
                    IdCarNbr = c.IdCarNbr,
                    Name = c.Name,
                    ID = c.Id,
                    Tel = c.Tel,
                    Zip = c.Zip,
                    Street = c.Street,
                    Nbr = c.Nbr

                };
                return cl;
            }

            return new ClientDTO();
        }

        public  DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }
        public override Task<Empty> AddClient(ClientDTO request, ServerCallContext context)
        {
            var client = new Client(){City = request.City,Zip=request.Zip,Street=request.Street,Nbr=request.Nbr,Tel=request.Tel,Name=request.Name,IdCarNbr=request.IdCarNbr,ForName=request.ForName,Email=request.Email, BirthDate = request.BirthDate.ToDateTime(),Country=request.Country,DrivingLicence=request.Drivinglicence};
         
            ClientValidator validationRules=new ClientValidator();
            if (validationRules.Validate(client).IsValid)
            {
                using var transaction = _clientDbContext.Database.BeginTransaction();
                {
                    try
                    {
                        var data = _clientDbContext.Clients.FirstOrDefault(i => i.Id.Equals(request.ID));
                        if (data != null)
                        {
                            _clientDbContext.Clients.Update(client);

                        }
                        else
                        {
                            _clientDbContext.Clients.Add(client);
                        }

                        _clientDbContext.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.StackTrace);
                        transaction.Rollback();
                        throw new RpcException(new Status(StatusCode.Cancelled,e.Message));

                    }
                }
            }
            else
            {
                _logger.LogError(client+ "Validation error");
                throw new RpcException(new Status(StatusCode.Cancelled, "Validation error")); }

           
         
            return Task.FromResult(new Empty());

        }
    }
}

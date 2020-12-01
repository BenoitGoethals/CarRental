using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceCar.data;
using GrpcServiceCar.Protos;
using GrpcServiceClient.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.helpers;
using Shared.model;
using Shared.validator;

namespace GrpcServiceCar.Services
{
    public class CarService : Cars.CarsBase
    {
        private readonly CarDbContext _carDbContext;
        private readonly ILogger<CarService> _logger;
        private readonly RepositoryCar _repositoryCar;
        public CarService(ILogger<CarService> logger, CarDbContext carDbContext, RepositoryCar repositoryCar)
        {
            _carDbContext = carDbContext;
            _logger = logger;
            _repositoryCar = repositoryCar;
        }



        public override async Task<CarDTO> GetCarByplate(CarRequestPlate request, ServerCallContext context)
        {
            try
            {

                var c = await _repositoryCar.GetCarByplate(request.Name);
                if (c == null) return new CarDTO();
                var cl = new CarDTO()
                {
                    Id = c.Id,
                    InCirculationDate = ConverterTime.UtcConverter(c.InCirculationDate),
                    LastMaintenace = ConverterTime.UtcConverter(c.LastMaintenace),
                    Km = c.Km,
                    NextMaintenace = ConverterTime.UtcConverter(c.NextMaintenace),
                    Operational = c.Operational,
                    PlateNbr = c.PlateNbr,
                    Remarks = c.Remarks,
                    TypeCar = (CarDTO.Types.TyepeOfCar)c.Type
                };

                return cl;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return new CarDTO();
            }

        }


        public override async Task<CarDTO> GetCaryId(CarRequestID request, ServerCallContext context)
        {
            try
            {
                var c = await _repositoryCar.GetCaryId(request.Id);
                if (c == null) return new CarDTO();
                var cl = new CarDTO()
                {
                    Id = c.Id,
                    InCirculationDate = ConverterTime.UtcConverter(c.InCirculationDate),
                    LastMaintenace = ConverterTime.UtcConverter(c.LastMaintenace),
                    Km = c.Km,
                    NextMaintenace = ConverterTime.UtcConverter(c.NextMaintenace),
                    Operational = c.Operational,
                    PlateNbr = c.PlateNbr,
                    Remarks = c.Remarks,
                    TypeCar = (CarDTO.Types.TyepeOfCar)c.Type
                };

                return cl;
                ;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return new CarDTO();
            }


        }

        public override async Task GetAllCar(Empty request, IServerStreamWriter<CarDTO> responseStream, ServerCallContext context)
        {
            try
            {
                var cars = await _repositoryCar.GetAllCar(context.CancellationToken);
                cars.ForEach(c => 
               {
                   responseStream.WriteAsync(new CarDTO()
                   {
                       Id = c.Id,
                       InCirculationDate = ConverterTime.UtcConverter(c.InCirculationDate),
                       LastMaintenace = ConverterTime.UtcConverter(c.LastMaintenace),
                       Km = c.Km,
                       NextMaintenace = ConverterTime.UtcConverter(c.NextMaintenace),
                       Operational = c.Operational,
                       PlateNbr = c.PlateNbr,
                       Remarks = c.Remarks,
                       TypeCar = (CarDTO.Types.TyepeOfCar)c.Type
                   });
               });
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
               
            }
         
        }

        public override async Task<Empty> AddCar(CarDTO request, ServerCallContext context)
        {
            var car = new Car()
            {
                InCirculationDate = request.InCirculationDate.ToDateTime(),
                LastMaintenace = request.LastMaintenace.ToDateTime(),
                NextMaintenace = request.NextMaintenace.ToDateTime(),
                Km = request.Km,
                Operational = request.Operational,
                PlateNbr = request.PlateNbr,
                Remarks = request.Remarks,
                Type = (TypeCar)request.TypeCar
            };
            CarValidator validationRules = new CarValidator();
            if ((await validationRules.ValidateAsync(car)).IsValid)
            {
                await _repositoryCar.AddCar(car);
            }
            else
            {
                _logger.LogError(car + "Validation error");
                throw new RpcException(new Status(StatusCode.Cancelled, "Validation error"));
            }

            return await Task.FromResult(new Empty());
        }

    
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceCar.data;
using GrpcServiceCar.Protos;
using GrpcServiceClient.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.helpers;
using Shared.model;
using Shared.validator;

namespace GrpcServiceCar.Services
{
    public class CarService : Cars.CarsBase
    {
        private CarDbContext CarDbContext;
        private readonly ILogger<CarService> _logger;
        public CarService(ILogger<CarService> logger, CarDbContext carDbContext)
        {
            CarDbContext = carDbContext;
            _logger = logger;
        }

        public override async Task<CarDTO> GetCarByplate(CarRequestPlate request, ServerCallContext context)
        {
            Car c = await CarDbContext.Cars.FirstOrDefaultAsync(t => t.PlateNbr.Equals(request.Name));
            if (c != null)
            {

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
            return new CarDTO();
        }


        public override async Task<CarDTO> GetCaryId(CarRequestID request, ServerCallContext context)
        {
            Car c = await CarDbContext.Cars.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));
            if (c != null)
            {

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
                    TypeCar = (CarDTO.Types.TyepeOfCar) c.Type
                };
            
                return cl;
            }
            return new CarDTO(); ;
        }

        public override async Task GetAllCar(Empty request, IServerStreamWriter<CarDTO> responseStream, ServerCallContext context)
        {
            await CarDbContext.Cars.ForEachAsync(c =>
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

        public override Task<Empty> AddCar(CarDTO request, ServerCallContext context)
        {
            Car car = new Car()
            {
                // Id=request.Id,
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
            if (validationRules.Validate(car).IsValid)
            {
                using var transaction = CarDbContext.Database.BeginTransaction();
                {
                    try
                    {
                        var data = CarDbContext.Cars.FirstOrDefault(i => i.Id.Equals(request.Id));
                        if (data != null)
                        {
                            CarDbContext.Cars.Update(car);

                        }
                        else
                        {
                            CarDbContext.Cars.Add(car);
                        }

                        CarDbContext.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.StackTrace);
                        transaction.Rollback();
                        throw new RpcException(new Status(StatusCode.Cancelled, e.Message));

                    }
                }
            }
            else
            {
                _logger.LogError(car + "Validation error");
                throw new RpcException(new Status(StatusCode.Cancelled, "Validation error"));
            }



            return Task.FromResult(new Empty());
        }
    }
}

using System;
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
        public CarService(ILogger<CarService> logger, CarDbContext carDbContext)
        {
            _carDbContext = carDbContext;
            _logger = logger;
        }

        public override async Task<CarDTO> GetCarByplate(CarRequestPlate request, ServerCallContext context)
        {
            try
            {
                var c = await _carDbContext.Cars.FirstOrDefaultAsync(t => t.PlateNbr.Equals(request.Name));
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
                var c = await _carDbContext.Cars.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));
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
                return new CarDTO(); ;
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
                await _carDbContext.Cars.ForEachAsync(c =>
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
            if ((await validationRules.ValidateAsync(car)).IsValid)
            {
                try
                {
                    await using var transaction = await _carDbContext.Database.BeginTransactionAsync();
                    {
                        try
                        {
                            var data = _carDbContext.Cars.FirstOrDefault(i => i.Id.Equals(request.Id));
                            if (data != null)
                            {
                                _carDbContext.Cars.Update(data);

                            }
                            else
                            {
                                await _carDbContext.Cars.AddAsync(car);
                            }

                            await _carDbContext.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.StackTrace);
                            await transaction.RollbackAsync();
                            throw new RpcException(new Status(StatusCode.Cancelled, e.Message));

                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                }


            }
            else
            {
                _logger.LogError(car + "Validation error");
                throw new RpcException(new Status(StatusCode.Cancelled, "Validation error"));
            }



            return await Task.FromResult(new Empty());
        }

        public override async Task<CarDTO> addTimeSlot(TimeSlotCarId request, ServerCallContext context)
        {
            await using var transaction = await _carDbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    var data = _carDbContext.Cars.FirstOrDefaultAsync(i => i.Id.Equals(request.IdCar));
                    if (data != null)
                    {
                        TimeSlot timeSlot = new TimeSlot() { EndSlot = request.Timeslot.EndSlot.ToDateTime(), StartSlot = request.Timeslot.StartSLot.ToDateTime(), Car = data.Result };
                        data.Result.TimeSlots.Add(timeSlot);
                        _carDbContext.Cars.Update(data.Result);
                        await _carDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        var ret = _carDbContext.Cars.Include(i => i.TimeSlots).FirstOrDefaultAsync(i => i.Id.Equals(request.IdCar)).Result;
                        List<TimeSLotCar> timeSLotCars = new List<TimeSLotCar>();
                        ret.TimeSlots.ForEach(t => timeSLotCars.Add(new TimeSLotCar() { StartSLot = ConverterTime.UtcConverter(t.StartSlot), EndSlot = ConverterTime.UtcConverter(t.EndSlot) }));
                        return new CarDTO()
                        {
                            Id = ret.Id,
                            InCirculationDate = ConverterTime.UtcConverter(ret.InCirculationDate),
                            LastMaintenace = ConverterTime.UtcConverter(ret.LastMaintenace),
                            Km = ret.Km,
                            NextMaintenace = ConverterTime.UtcConverter(ret.NextMaintenace),
                            Operational = ret.Operational,
                            PlateNbr = ret.PlateNbr,
                            Remarks = ret.Remarks,
                            TypeCar = (CarDTO.Types.TyepeOfCar)ret.Type,
                            TimeSLots = { timeSLotCars }
                        };
                    }
                    return null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                    await transaction.RollbackAsync();
                    throw new RpcException(new Status(StatusCode.Cancelled, e.Message));

                }
            }

        }

        public override async Task<CarDTO> addTimeSlotByName(TimeSlotCarName request, ServerCallContext context)
        {
            await using var transaction = await _carDbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    var data = _carDbContext.Cars.FirstOrDefaultAsync(i => i.Id.Equals(request.IdName));

                    if (data != null)
                    {
                        TimeSlot timeSlot = new TimeSlot() { EndSlot = request.Timeslot.EndSlot.ToDateTime(), StartSlot = request.Timeslot.StartSLot.ToDateTime(), Car = data.Result };
                        data.Result.TimeSlots.Add(timeSlot);
                        _carDbContext.Cars.Update(data.Result);
                        await _carDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        var ret = _carDbContext.Cars.Include(i => i.TimeSlots).FirstOrDefaultAsync(i => i.Id.Equals(request.IdName)).Result;
                        List<TimeSLotCar> timeSLotCars = new List<TimeSLotCar>();
                        ret.TimeSlots.ForEach(t => timeSLotCars.Add(new TimeSLotCar() { StartSLot = ConverterTime.UtcConverter(t.StartSlot), EndSlot = ConverterTime.UtcConverter(t.EndSlot) }));
                        return new CarDTO()
                        {
                            Id = ret.Id,
                            InCirculationDate = ConverterTime.UtcConverter(ret.InCirculationDate),
                            LastMaintenace = ConverterTime.UtcConverter(ret.LastMaintenace),
                            Km = ret.Km,
                            NextMaintenace = ConverterTime.UtcConverter(ret.NextMaintenace),
                            Operational = ret.Operational,
                            PlateNbr = ret.PlateNbr,
                            Remarks = ret.Remarks,
                            TypeCar = (CarDTO.Types.TyepeOfCar)ret.Type,

                            TimeSLots = { timeSLotCars }

                        };
                    }

                    return null;

                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                    await transaction.RollbackAsync();
                    throw new RpcException(new Status(StatusCode.Cancelled, e.Message));

                }
            }


        }
    }
}

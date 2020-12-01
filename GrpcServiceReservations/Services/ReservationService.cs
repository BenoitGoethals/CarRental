using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServiceCar.Protos;
using GrpcServiceReservations.data;
using GrpcServiceReservations.Protos;
using Microsoft.Extensions.Logging;
using Shared.helpers;
using Shared.model;
using CarRequestID = GrpcServiceReservations.Protos.CarRequestID;


namespace GrpcServiceReservations.Services
{
    public class ReservationService: TimeSlots.TimeSlotsBase
    {

        private readonly RepositoryReservation _repositoryReservation;
        private readonly ILogger<ReservationService> _logger;
        public ReservationService(ILogger<ReservationService> logger, RepositoryReservation repositoryReservation)
        {
            _repositoryReservation = repositoryReservation;
            _logger = logger;
        }

        public override async Task<StatusCrud> AddTimeSlot(TimeSlotCarId request, ServerCallContext context)
        {
            try
            {
              
                TimeSlot timeSlot = new TimeSlot() { EndSlot = request.Timeslot.EndSlot.ToDateTime(), StartSlot = request.Timeslot.StartSLot.ToDateTime(), CarId = request.IdCar };
                await _repositoryReservation.AddTimeSlot(timeSlot);

                return new StatusCrud(){Added=true};

            }
            catch (Exception e)
            {

                _logger.LogError(e.StackTrace);
                return new StatusCrud() { Added = false };

                //   throw new RpcException(new Grpc.Core.Status(StatusCode.Cancelled, e.Message));

            }
        }

        public override async Task<TimeSLotCollection> GetCarTimeSLotsById(CarRequestID request, ServerCallContext context)
        {
            try
            {
                var liAllTimeSlots= await _repositoryReservation.AllTimeSlots(request.Id);
                TimeSLotCollection timeSLotCollection=new TimeSLotCollection();
                liAllTimeSlots.ForEach(t => timeSLotCollection.TimeSLots.Add(new TimeSLotCar(){Id=t.CarId,EndSlot=ConverterTime.UtcConverter(t.EndSlot),StartSLot=ConverterTime.UtcConverter(t.StartSlot)}));
                return timeSLotCollection;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }
        }

        public override async Task<TimeSlotDelete> DeleteTimesloteCar(TimeSlotCarId request, ServerCallContext context)
        {
            try
            {

                var deleted = await _repositoryReservation.Remove(request.IdCar, request.Timeslot.Id);

                return new TimeSlotDelete() { Deleted = deleted };
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return new TimeSlotDelete() { Deleted = false };
            }
        }


    }
}


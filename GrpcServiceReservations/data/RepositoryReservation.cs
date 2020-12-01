using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.model;

namespace GrpcServiceReservations.data
{
    public class RepositoryReservation
    {


        private readonly ReservationDbContext _reservationDbContext;
        private readonly ILogger<RepositoryReservation> _logger;
        public RepositoryReservation(ILogger<RepositoryReservation> logger, ReservationDbContext carDbContext)
        {
            _reservationDbContext = carDbContext;
            _logger = logger;
        }



        public async Task<bool> Remove(int requestIdCar, int timeSlotId)
        {
            await using var transaction = await _reservationDbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    var data = await _reservationDbContext.TimeSlots.FirstOrDefaultAsync(i => i.Id.Equals(timeSlotId) && i.CarId.Equals(requestIdCar));
                    if (data != null)
                    {
                        _reservationDbContext.TimeSlots.Remove(data);
                        await _reservationDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }

                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                    await transaction.RollbackAsync();
                    return false;

                }
            }
            return false;
        }

        public async Task AddTimeSlot(TimeSlot newtTmeSlot)
        {
            await using var transaction = await _reservationDbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    await _reservationDbContext.TimeSlots.AddAsync(newtTmeSlot);
                    await _reservationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                    await transaction.RollbackAsync();


                }
            }

        }


        public async Task<TimeSlot> GetTimeSlot(int id)
        {
            try
            {
                return await _reservationDbContext.TimeSlots.FirstAsync(t => t.Id.Equals(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }

        }


        public async Task<List<TimeSlot>> AllTimeSlots(int idCar)
        {
            try
            {
                return await _reservationDbContext.TimeSlots.Where(i => i.CarId.Equals(idCar)).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }

        }
    }
}

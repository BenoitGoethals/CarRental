using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceCar.Protos;
using GrpcServiceCar.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.helpers;
using Shared.model;
using Shared.validator;

namespace GrpcServiceCar.data
{
    public class RepositoryCar
    {
        private readonly CarDbContext _carDbContext;
        private readonly ILogger<RepositoryCar> _logger;
        public RepositoryCar(ILogger<RepositoryCar> logger, CarDbContext carDbContext)
        {
            _carDbContext = carDbContext;
            _logger = logger;
        }



        public async Task<Car> GetCarByplate(string car)
        {
            try
            {
                var c = await _carDbContext.Cars.FirstOrDefaultAsync(t => t.PlateNbr.Equals(car));
                return c;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }

        }


        public async Task<Car> GetCaryId(int car)
        {
            try
            {
                var c = await _carDbContext.Cars.FirstOrDefaultAsync(t => t.Id.Equals(car));
                return c;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }


        }

        public async Task<List<Car>> GetAllCar(CancellationToken contextCancellationToken)
        {
            try
            {
                return await _carDbContext.Cars.ToListAsync(contextCancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return null;
            }

        }

        public async Task AddCar(Car car)
        {
            
            CarValidator validationRules = new CarValidator();
            if ((await validationRules.ValidateAsync(car)).IsValid)
            {
                try
                {
                    await using var transaction = await _carDbContext.Database.BeginTransactionAsync();
                    {
                        try
                        {
                            var data = _carDbContext.Cars.FirstOrDefault(i => i.Id.Equals(car.Id));
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
               
            }

            
        }


        public async Task<bool> Remove(int requestIdCar)
        {
            await using var transaction = await _carDbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    var data = await _carDbContext.Cars.FirstOrDefaultAsync(i => i.Id.Equals(requestIdCar));
                    if (data != null)
                    {
                        _carDbContext.Cars.Remove(data);
                        await _carDbContext.SaveChangesAsync();
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

    }
}
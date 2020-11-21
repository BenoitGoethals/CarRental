using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GrpcServiceCar.Services
{
    public class ClientService
    {
        private readonly ILogger<ClientService> _logger;
        public ClientService(ILogger<ClientService> logger)
        {
            _logger = logger;
        }
    }
}

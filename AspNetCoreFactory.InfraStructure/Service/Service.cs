using SharedDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Service
{
    public class Service : IService
    {
        private readonly IServiceManager _serviceManager;
        public Service(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        public void CalculateStatistics()
        {

        }
    }
}

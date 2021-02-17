using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Service
{
    public interface IService
    {
        //Task CalculateStatisticsAsync();
        //Task CalculateRecordCountsAsync(List model);

        void CalculateStatistics();
        // CalculateRecordCounts(List model);
    }
}

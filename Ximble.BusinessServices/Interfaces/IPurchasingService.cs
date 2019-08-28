using System;
using System.Collections.Generic;

namespace Ximble.BusinessServices.Interfaces
{
    public interface IPurchasingService
    {
        IEnumerable<object> GetSumOfTraffic(DateTime startDate, DateTime endDate);
        IEnumerable<object> GetNumberOfSoldUnits(DateTime startDate, DateTime endDate);
         
    }
}

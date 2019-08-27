using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ximble.BusinessServices.Interfaces
{
    public interface IPurchasingService
    {
        decimal GetSumOfTraffic(DateTime startDate, DateTime endDate);
        int GetNumberOfSoldProduct(DateTime startDate, DateTime endDate);
         
    }
}

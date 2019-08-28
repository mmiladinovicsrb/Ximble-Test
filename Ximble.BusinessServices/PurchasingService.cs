using System;
using System.Collections.Generic;
using System.Linq;
using Ximble.BusinessServices.Interfaces;
using Ximble.DataModel;

namespace Ximble.BusinessServices
{
    public class PurchasingService : IPurchasingService
    {
        private readonly UnitOfWork unitOfWork;

        public PurchasingService()
        {
            unitOfWork = new UnitOfWork();
        }

        public IEnumerable<object> GetNumberOfSoldUnits(DateTime startDate, DateTime endDate)
        {
            // 
            var purchasingOrder = unitOfWork.PurchaseOrderDetailRepository.GetAll().GroupBy(g => g.Product.Name).Select(p=> new { ProductName = p.Key, TotalSoldUnits = p.Count()});
            return purchasingOrder;
        }

        public IEnumerable<object> GetSumOfTraffic(DateTime startDate, DateTime endDate)
        {
            var sumOfTraffic = unitOfWork.PurchaseOrderDetailRepository.GetAll().GroupBy(g => g.Product.Name).Select(p => new { ProductName = p.Key, LineOfTotal = p.Sum(item => item.LineTotal) }); 
            return sumOfTraffic;
        }
    }
}

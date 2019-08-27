using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int GetNumberOfSoldProduct(DateTime startDate, DateTime endDate)
        {
            var purchasingOrder = unitOfWork.PurchaseOrderDetailRepository.GetAll();
            var count = purchasingOrder.Select(x => x.OrderQty).Count();
            
            return count;
        }

        public decimal GetSumOfTraffic(DateTime startDate, DateTime endDate)
        {
            var sumOfTraffic = unitOfWork.PurchaseOrderDetailRepository.GetAll().Sum(x => x.OrderQty);
            return sumOfTraffic;
        }
    }
}

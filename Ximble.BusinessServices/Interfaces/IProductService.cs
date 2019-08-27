using System;
using System.Linq;
using Ximble.BusinessEntities;

namespace Ximble.BusinessServices.Interfaces
{
    public interface IProductService
    {
        ProductEntity GetByName(string productName);
        IQueryable<ProductEntity> GetBySellStartDate(DateTime sellStartDate);
        IQueryable<ProductDescriptionEntity> GetByKeywords(string keyWord);
    }
}

using System.Collections.Generic;
using Ximble.BusinessServices.Interfaces;
using Ximble.DataModel;

namespace Ximble.BusinessServices.Tests
{
    public class ProductServiceTest
    {
        private IProductService _productService;
        private UnitOfWork _unitOfWork;
        private List<Product> _products;
        private GenericRepository<Product> _productRepository;
        private AdventureWorks2017Entities _dbEntities;
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using Ximble.BusinessEntities;
using Ximble.BusinessServices.Interfaces;
using Ximble.DataModel;

namespace Ximble.BusinessServices
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork unitOfWork;

        public ProductService()
        {
            unitOfWork = new UnitOfWork();
        }

        public IQueryable<ProductDescriptionEntity> GetByKeywords(string keyWord)
        {
            var productsDesc = unitOfWork.ProductDescriptionRepository.GetManyQueryable(x=>x.Description.Contains(keyWord));

            if (productsDesc != null)
            {
                IQueryable<ProductDescriptionEntity> listToReturn;
                Mapper.CreateMap<ProductDescription, ProductDescriptionEntity>();

                listToReturn = productsDesc.Project().To<ProductDescriptionEntity>();
                return listToReturn;
            }

            return null;

        }

        public IQueryable<ProductEntity> GetBySellStartDate(DateTime sellStartDate)
        {
            var products = unitOfWork.ProductRepository.GetManyQueryable(x => x.SellStartDate == sellStartDate);

            if (products != null)
            {
                IQueryable<ProductEntity> listToReturn;
                Mapper.CreateMap<Product, ProductEntity>();
                listToReturn = products.Project().To<ProductEntity>();
                return listToReturn;
            }

            return null;
        }

        public ProductEntity GetByName(string productName)
        {
            var product = unitOfWork.ProductRepository.Get(x => x.Name == productName);
            if(product != null)
            {
                Mapper.CreateMap<Product, ProductEntity>();
                var productModel = Mapper.Map<Product, ProductEntity>(product);
                return productModel;
            }

            return null;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Ximble.BusinessEntities;
using Ximble.WebApi.Controllers;

namespace Ximble.WebApi.Tests
{
    [TestClass]
    public class ProductUnitTest
    {
        [TestMethod]
        public void GetProductByName()
        {
            var controller = new ProductController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();

            var response = controller.GetByName("Adjustable Race");
            ProductEntity product;
            Assert.IsTrue(response.TryGetContentValue<ProductEntity>(out product));
            Assert.AreEqual("AR-5381", product.ProductNumber);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ximble.BusinessEntities;
using Ximble.BusinessServices;
using Ximble.BusinessServices.Interfaces;

namespace Ximble.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        [Route("api/product/get-by-name")]
        public HttpResponseMessage GetByName([FromBody] string productName)
        {
            var product = _productService.GetByName(productName);

            if (product != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found with this name");

        }

        [HttpGet]
        [Route("api/product/get-by-date")]
        public HttpResponseMessage GetBySellStartDate([FromUri]PaginatedModel pagingModel, [FromBody] string sellStartDate)
        {
            var date = Convert.ToDateTime(sellStartDate);
            var products = _productService.GetBySellStartDate(date);

            if(pagingModel != null)
            {
                int count = products.Count();

                int CurrentPage = pagingModel.PageNumber;
                int PageSize = pagingModel.PageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                var previousPage = CurrentPage > 1 ? "Yes" : "No";
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                // object which we are going to send in header   
                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                // Setting Header  
                HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            }
           
            if (products != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found with this name");
        }

        [HttpGet]
        [Route("api/product/get-by-keyword")]
        public HttpResponseMessage GetByKeyWord([FromUri]PaginatedModel pagingModel, [FromBody] string keyword)
        {
            var products = _productService.GetByKeywords(keyword);

            if (pagingModel != null)
            {
                int count = products.Count();

                int CurrentPage = pagingModel.PageNumber;
                int PageSize = pagingModel.PageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                var previousPage = CurrentPage > 1 ? "Yes" : "No";
                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                // Setting Header  
                HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            }
            
            if (products != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found with this name");
        }

    }
}

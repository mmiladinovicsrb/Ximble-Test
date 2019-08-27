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
        public HttpResponseMessage GetBySellStartDate([FromUri]PaginatedModel pagingparametermodel, [FromBody] string sellStartDate)
        {
            var date = Convert.ToDateTime(sellStartDate);
            var products = _productService.GetBySellStartDate(date);
            int count = products.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
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

            if (products != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found with this name");
        }
        [HttpGet]
        [Route("api/product/get-by-keyword")]
        public HttpResponseMessage GetByKeyWord([FromUri]PaginatedModel pagingparametermodel, [FromBody] string keyword)
        {
            var products = _productService.GetByKeywords(keyword);
            int count = products.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // calculating totalpage by dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // list of products after applying paging   
            var items = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
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

            if (products != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found with this name");
        }

    }
}

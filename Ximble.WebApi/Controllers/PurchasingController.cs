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
    public class PurchasingController : ApiController
    {
        private readonly IPurchasingService _purchasingService;

        public PurchasingController()
        {
            _purchasingService = new PurchasingService();
        }

        [HttpGet]
        [Route("api/purchasing/get-sum")]
        public HttpResponseMessage GetSumOfTraffic([FromUri]PaginatedModel pagingModel)
        {
            //TO DO - check which date parameters should be used  - [SellStartDate],[SellEndDate] ???
        var lineTotal = _purchasingService.GetSumOfTraffic(DateTime.UtcNow, DateTime.UtcNow);

            if(pagingModel != null)
            {
                int count = lineTotal.Count();

                int CurrentPage = pagingModel.PageNumber;
                int PageSize = pagingModel.PageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = lineTotal.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
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

            if (lineTotal != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, lineTotal);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed");

        }

        [HttpGet]
        [Route("api/purchasing/get-num")]
        public HttpResponseMessage GetNumberOfSoldUnits([FromUri]PaginatedModel pagingModel)
        {
            //TO DO - check which date parameters should be used  - [SellStartDate],[SellEndDate] ???
            var soldUnits = _purchasingService.GetNumberOfSoldUnits(DateTime.UtcNow, DateTime.UtcNow);

            if (pagingModel != null)
            {
                int count = soldUnits.Count();

                int CurrentPage = pagingModel.PageNumber;
                int PageSize = pagingModel.PageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = soldUnits.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
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


            if (soldUnits != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, soldUnits);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There were no sold units");
        }
    }
}

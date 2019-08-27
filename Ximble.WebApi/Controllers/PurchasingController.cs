using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public HttpResponseMessage GetSumOfTraffic()
        {
            var totalSum = _purchasingService.GetSumOfTraffic(DateTime.UtcNow, DateTime.UtcNow);

            if (totalSum > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, totalSum);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Zero bato");

        }
    }
}

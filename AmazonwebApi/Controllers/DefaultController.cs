using AmazonwebApi.Persistance.Repositary;
using AmazonwebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmazonwebApi.Controllers
{
    public class DefaultController : ApiController
    {
        private IProductServices productServicesobj;

        public DefaultController()
        {
            productServicesobj = new ProductServices(new ProductRepositary());
        }
        [HttpPost]
        [Route("api/Default/GetProductCategory")]

        public IHttpActionResult GetProductCategory()
        {
            return Json(productServicesobj.GetProductCategory());
        }
    }
}

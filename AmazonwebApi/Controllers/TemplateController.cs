using AmazonwebApi.Persistance.Repositary;
using AmazonwebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;

namespace AmazonwebApi.Controllers
{
    public class TemplateController : ApiController
    {


        private ITemplateServices TemplateServicesobj;

        public TemplateController()
        {
            TemplateServicesobj = new TemplateServices(new TemplateRepositary());
        }
        [HttpPost]
        [Route("api/Template/GetTemplates")]

        public IHttpActionResult GetTemplates()
        {
            return Json(TemplateServicesobj.GetTemplates());
        }

        // GET: api/Template
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Template/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Template
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Template/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Template/5
        public void Delete(int id)
        {
        }
    }
}

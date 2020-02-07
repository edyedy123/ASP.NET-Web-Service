using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_3.Controllers
{
    public class InvoiceLineController : ApiController
    {
        Manager m = new Manager();

        // GET: api/InvoiceLine
        public IHttpActionResult Get()
        {
            return Ok(m.InvoiceLineGetAll());
        }

        // GET: api/InvoiceLine/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InvoiceLine
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/InvoiceLine/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InvoiceLine/5
        public void Delete(int id)
        {
        }
    }
}

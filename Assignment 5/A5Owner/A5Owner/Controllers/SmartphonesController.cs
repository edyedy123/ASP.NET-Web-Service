using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace A5Owner.Controllers
{
    public class SmartphonesController : ApiController
    {
        // Attention 16 - Reference to the manager object
        private Manager m = new Manager();

        // Attention 17 - All return types in this assignment will be IHttpActionResult

        // Attention 21 - Example data for the entity bodies (which can be used in Fiddler) is included in the EntityBodyData.js file in the project root

        // GET: api/Smartphones
        public IHttpActionResult Get()
        {
            // Attention 18 - Get all - can do this in one line of code, or two

            var c = m.SmartphoneGetAll();

            return Ok(c);
        }

        // GET: api/Smartphones/5
        public IHttpActionResult Get(int? id)
        {
            // Attention 19 - Get one by identifier - guard against a null identifier

            // Attempt to fetch the object
            var o = m.SmartphoneGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Smartphones
        public IHttpActionResult Post([FromBody]SmartphoneAdd newItem)
        {
            // Attention 20 - Add new - notice the various checks

            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.SmartphoneAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        /*
        // PUT: api/Smartphones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Smartphones/5
        public void Delete(int id)
        {
        }
        */
    }
}

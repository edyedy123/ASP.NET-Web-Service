using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_3.Controllers
{
    public class EmployeeController : ApiController
    {
        Manager m = new Manager();

        // GET: api/Employee
        public IHttpActionResult Get()
        {
            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employee/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.EmployeeGetByIdWithMedia(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            // Otherwise, continue...

            // Attention 06 - Here is the content negotiation code

            // Look for an Accept header that starts with "image"

            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));

            if (imageHeader == null)
            {
                // Normal processing for a JSON result
                // Remove the "Photo" property
                return Ok(m.mapper.Map<EmployeeWithMedia>(o));
            }
            else
            {
                // Special processing for an image result

                // Confirm that a media item exists
                if (o.PhotoLength > 0)
                {
                    // Return the result, using the custom media formatter
                    return Ok(o.Photo);
                }
                else
                {
                    // Otherwise, return "not found"
                    // Yes, this is correct. Read the RFC: https://tools.ietf.org/html/rfc7231#section-6.5.4
                    return NotFound();
                }
            }
        }

        // POST: api/Employee
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null)
            {
                return BadRequest("Invalid request URI");
            }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to add the new object
                var addedItem = m.EmployeeAdd(newItem);

                // Notice the ApiController convenience methods
                if (addedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    // HTTP 201 with the new object in the entity body
                    // Notice how to create the URI for the Location header

                    var uri = Url.Link("DefaultApi", new { id = addedItem.EmployeeId });
                    return Created<EmployeeBase>(uri, addedItem);
                }
            }
            else
            {
                // HTTP 400
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }
        // PUT: api/Employee/5/SetSupervisor
        [Route("api/employee/{id}/setsupervisor")]
        public void PutSetSupervisor(int id, [FromBody]EmployeeSupervisor item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.Employee) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.SetEmployeeSupervisor(item);
            }
            else
            {
                return;
            }
        }
        // PUT: api/Employee/5/SetDirectReports
        [Route("api/employee/{id}/SetDirectReports")]
        public void PutSetDirectReports(int id, [FromBody]EmployeeDirectReports item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.EmployeeId) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.SetDirectReports(item);
            }
            else
            {
                return;
            }
        }
        // PUT: api/Employee/5/ClearDirectReports
        [Route("api/employee/{id}/ClearDirectReports")]
        public void PutClearDirectReports(int id, [FromBody]EmployeeDirectReports item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.EmployeeId) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.ClearDirectReports(item);
            }
            else
            {
                return;
            }
        }
        // PUT: api/Employee/5/setphoto
        // Notice the use of the [HttpPut] attribute, which is an alternative to the method name starting with "Put..."
        [Route("api/employee/{id}/setphoto")]
        [HttpPut]
        public IHttpActionResult EmployeePhoto(int id, [FromBody]byte[] photo)
        {
            if (photo.Length > 100000)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.EmployeeSetPhoto(id, contentType, photo))
            {
                // By convention, we have decided to return HTTP 204
                // It's a 'success' code, but there's no content for a 'command' task
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                // Uh oh, some error happened, so tell the requestor
                return BadRequest("Unable to set the photo");
            }
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }

    }
}

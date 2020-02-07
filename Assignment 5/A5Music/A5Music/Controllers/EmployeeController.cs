using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace A5Music.Controllers
{
    public class EmployeeController : ApiController
    {
        Manager m = new Manager();

        // GET: api/Employee
        [Authorize(Roles = "Employee")]
        [AuthorizeClaim(ClaimType = "OU", ClaimValue = "Toronto")]
        public IHttpActionResult Get()
        {
            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employee/5
        [Authorize(Roles = "Employee")]
        [AuthorizeClaim(ClaimType = "OU", ClaimValue = "Toronto")]
        [AuthorizeClaim(ClaimType = "Task", ClaimValue = "EmployeeView")]
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.EmployeeGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }
        [Authorize(Roles = "Employee")]
        [AuthorizeClaim(ClaimType = "Task", ClaimValue = "EmployeeEdit")]
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


        // PUT: api/Employee/5/SetSupervisor
        [Authorize(Roles = "Employee")]
        [AuthorizeClaim(ClaimType = "Task", ClaimValue = "SupervisorTasks")]
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
       

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }

    }
}

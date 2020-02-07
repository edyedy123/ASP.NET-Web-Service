using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_3.Controllers
{
    public class AlbumController : ApiController
    {
        Manager m = new Manager();

        // GET: api/Album
        public IHttpActionResult Get()
        {
            return Ok(m.AlbumGetAll());
        }

        // GET: api/Album/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.AlbumGetByIdWithMedia(id.GetValueOrDefault());

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
                return Ok(m.mapper.Map<AlbumWithMedia>(o));
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

        // POST: api/Album
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Album/5/setphoto
        // Notice the use of the [HttpPut] attribute, which is an alternative to the method name starting with "Put..."
        [Route("api/album/{id}/setphoto")]
        [HttpPut]
        public IHttpActionResult AlbumPhoto(int id, [FromBody]byte[] photo)
        {
            if (photo.Length > 100000)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.AlbumSetPhoto(id, contentType, photo))
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

        // DELETE: api/Album/5
        public void Delete(int id)
        {
        }
    }
}

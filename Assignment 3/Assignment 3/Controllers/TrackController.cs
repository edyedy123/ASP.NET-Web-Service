using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_3.Controllers
{
    public class TrackController : ApiController
    {
        Manager m = new Manager();

        // GET: api/Track
        public IHttpActionResult Get()
        {
            return Ok(m.TrackGetAll());
        }

        // GET: api/Track/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.TrackGetByIdWithMedia(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            // Otherwise, continue...

            // Attention 06 - Here is the content negotiation code

            // Look for an Accept header that starts with "image"

            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("audio/"));

            if (imageHeader == null)
            {
                // Normal processing for a JSON result
                // Remove the "Photo" property
                return Ok(m.mapper.Map<TrackWithMedia>(o));
            }
            else
            {
                // Special processing for an image result

                // Confirm that a media item exists
                if (o.AudioLength > 0)
                {
                    // Return the result, using the custom media formatter
                    return Ok(o.Audio);
                }
                else
                {
                    // Otherwise, return "not found"
                    // Yes, this is correct. Read the RFC: https://tools.ietf.org/html/rfc7231#section-6.5.4
                    return NotFound();
                }
            }
        }

        // POST: api/Track
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Track/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Track/5
        public void Delete(int id)
        {
        }
        // PUT: api/Track/5/setAudio
        // Notice the use of the [HttpPut] attribute, which is an alternative to the method name starting with "Put..."
        [Route("api/track/{id}/setaudio")]
        [HttpPut]
        public IHttpActionResult TrackAudio(int id, [FromBody]byte[] Audio)
        {
            //made this higher than 200kb becasue I coulent find smaller clips
            if (Audio.Length > 9000000)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            // Get the Content-Type header from the request
            var contentType = Request.Content.Headers.ContentType.MediaType;

            // Attempt to save
            if (m.TrackSetAudio(id, contentType, Audio))
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
    }
}

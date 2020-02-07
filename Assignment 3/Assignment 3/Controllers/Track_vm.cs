using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Controllers
{
    public class TrackAdd
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }
 
    }
    public class TrackBase : TrackAdd
    {
        public int TrackId { get; set; }

    }
    public class TrackWithMedia : TrackBase
    {
        public byte[] Audio { get; set; }

        public int AudioLength { get; set; }

        public string ContentType { get; set; }

    }
}
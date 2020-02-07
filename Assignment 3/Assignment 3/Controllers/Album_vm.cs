using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Controllers
{
    public class AlbumAdd
    {
        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Range(1, UInt32.MaxValue)]
        public int ArtistId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public int AlbumId { get; set; }

    }
    public class AlbumWithMedia : AlbumBase
    {
        public byte[] Photo { get; set; }

        public int PhotoLength { get; set; }

        public string ContentType { get; set; }
    }

}
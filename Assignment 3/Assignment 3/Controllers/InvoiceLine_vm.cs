using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Controllers
{
    public class InvoiceLineBase
    {
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        public int TrackId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        //public virtual Invoice Invoice { get; set; }

        public String TrackName { get; set; }
        public String TrackComposer { get; set; }

        public String TrackAlbumTitle { get; set; }
        public String TrackAlbumArtistName { get; set; }
        public String TrackMediaTypeName { get; set; }

    }
}
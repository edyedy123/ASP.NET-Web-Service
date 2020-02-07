using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace A5Owner.Controllers
{
    // Attention 08 - Resource (view) models for use cases

    public class SmartphoneAdd
    {
        public SmartphoneAdd()
        {
            DateReleased = DateTime.Now.AddMonths(-1);
        }

        [Required, StringLength(100)]
        public string Manufacturer { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        public DateTime DateReleased { get; set; }

        // Attention 09 - Data annotations for the resource model classes

        [Range(3.0, 10.0)]
        public double ScreenSize { get; set; }

        [Range(10, 10000)]
        public int MSRP { get; set; }
    }

    // Attention 10 - Probably a good idea to use inheritance in this simple situation
    public class SmartphoneBase : SmartphoneAdd
    {
        public int Id { get; set; }

        [Required, StringLength(128)]
        public string Owner { get; set; }
    }
}

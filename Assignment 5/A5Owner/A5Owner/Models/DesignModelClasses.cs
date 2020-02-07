using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A5Owner.Models
{

    // Add your app's domain or design model classes below
    // Delete the example "Gizmo" class...

    public class Gizmo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Smartphone
    {
        // Attention 04 - Example properties, 5 or more, names can vary but should follow C# coding conventions

        public Smartphone()
        {
            // Attention 05 - Must use a default constructor when we have dates and collections

            DateReleased = DateTime.Now.AddMonths(-1);
        }

        public int Id { get; set; }

        [Required, StringLength(128)]
        public string Owner { get; set; }

        // Attention 06 - Must use data annotations, where appropriate

        [Required, StringLength(100)]
        public string Manufacturer { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        public DateTime DateReleased { get; set; }
        public double ScreenSize { get; set; }
        public int MSRP { get; set; }
    }
}
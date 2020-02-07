namespace A5Owner.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        // Add DbSet properties here...

        // For example... (the following is just a how-to reminder)
        //public virtual DbSet<Gizmo> Gizmos { get; set; }

        public virtual DbSet<Smartphone> Smartphones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace A5Music.Controllers
{
    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            BirthDate = DateTime.Now;
            HireDate = DateTime.Now;
        }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
    }

        public class EmployeeBase: EmployeeAdd
    {
        public int EmployeeId { get; set; }
  
    }
     

    public class EmployeeSupervisor
    {
        public int Employee { get; set; }
        public int Supervisor { get; set; }
    }
    
}
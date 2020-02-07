using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Controllers
{
    public class InvoiceBase
    {
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        public decimal Total { get; set; }
  
       
    }

    public class InvoiceWithData : InvoiceBase
    {
        public InvoiceWithData()
        {
            InvoiceLines = new HashSet<InvoiceLineBase>();
        }
        public virtual ICollection<InvoiceLineBase> InvoiceLines { get; set; }

        public String CustomerFirstName { get; set; }
        public String CustomerLastName { get; set; }

        public String CustomerEmployeeFirstName { get; set; }
        public String CustomerEmployeeLastName { get; set; }

        public byte[] PDF { get; set; }

        public int PDFLength { get; set; }

        public string ContentType { get; set; }
    }
   

}
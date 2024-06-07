using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Premia_API.Entities
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public string Type { get; set; }
        public int OwnerID { get; set; } 
        public string CaseNumber { get; set; }
        public double Income { get; set; }
        public float TimeConsumed { get; set; }
        public int Month { get; set; }
        public string InvoiceStatus { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? DeleteDate { get; set; } = null;
        public bool isDeleted { get; set; } = false;   
        public DateTime? SettlementDate { get; set; } = null;
        public DateTime? ModifyDate { get; set; } = null;
        public bool isNewDocument { get; set; } = true;
        public bool? PreAccept { get; set; } = null; 
        public bool? Accepted { get; set; } = null;
    }
}

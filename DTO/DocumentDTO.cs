namespace Premia_API.DTO
{
        public class DocumentDTO
        {
            public int DocumentID { get; set; }
            public string InvoiceNumber { get; set; }
            public int CustomerID { get; set; }
            public string Type { get; set; }
            public int OwnerID { get; set; }
            public string CaseNumber { get; set; }
            public double Income { get; set; }
            public float TimeConsumed { get; set; }
            public double Drive { get; set; }
            public int Month { get; set; }
            public string InvoiceStatus { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime? DeleteDate { get; set; }
            public bool isDeleted { get; set; }
            public DateTime? SettlementDate { get; set; }
            public DateTime? ModifyDate { get; set; }
            public bool isNewDocument { get; set; }
            public bool? PreAccept { get; set; }
            public bool? Accepted { get; set; }
        }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Premia_API.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DeleteDate { get; set; } = null;
        public bool isDeleted { get; set; } = false;
        public List<CustomerUser> CustomerUsers { get; set; } = new List <CustomerUser>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Premia_API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public  string Name  { get; set; }
        public  string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public int? SupervisorId { get; set; } = null;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public DateTime? DeleteDate { get; set; } = null;
        public bool isNormalUser { get; set; } = true;
        public bool isSuperUser { get; set; } = false;
        public bool isSupervisor { get; set; } = false;
        public List<CustomerUser> CustomerUsers { get; set; } = new List<CustomerUser>();

    }
}
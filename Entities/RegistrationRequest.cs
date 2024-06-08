using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Premia_API.Entities
{
    public class RegistrationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public int SupervisorId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool isApproved { get; set; } = false;
    }
}

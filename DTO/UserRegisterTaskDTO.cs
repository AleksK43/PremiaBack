using System.Reflection.Metadata;

namespace Premia_API.DTO
{
    public class UserRegisterTaskDTO
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Deprtment { get; set; }
        public required int SupervisorId { get; set; }

    }
}

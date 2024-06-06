namespace Premia_API.Entities
{
    public class CustomerUser
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

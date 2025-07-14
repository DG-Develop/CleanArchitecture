namespace ECommerce.Domain.EcommerceDbEntities
{
    public class Users
    {
        public int Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
    }
}

namespace ECommerce.Application.Commons.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string email);
    }
}

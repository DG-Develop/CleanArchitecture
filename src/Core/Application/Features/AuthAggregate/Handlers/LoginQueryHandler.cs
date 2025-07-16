using ECommerce.Application.Commons.Interfaces;
using ECommerce.Application.Features.AuthAggregate.Queries;
using ECommerce.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.AuthAggregate.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilter(u => u.Email == request.Email && u.PasswordHash == request.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }


            // Generate JWT token for the authenticated user
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);

            return token; // Replace with actual token generation logic
        }
    }
}

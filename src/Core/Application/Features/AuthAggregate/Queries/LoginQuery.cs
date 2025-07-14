using MediatR;

namespace ECommerce.Application.Features.AuthAggregate.Queries
{
    public record LoginQuery(string Email, string Password) : IRequest<string>;
}

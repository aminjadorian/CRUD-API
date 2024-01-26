using MediatR;
namespace Application.Users.Get;
public record GetUserQuery(Guid Id):IRequest<UserResponse>;

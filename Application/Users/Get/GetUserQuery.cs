using MediatR;
namespace Application.Users.Get;
public sealed record GetUserQuery(Guid Id):IRequest<UserResponse>;

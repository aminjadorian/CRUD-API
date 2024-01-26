using MediatR;

namespace Application.Users.Get;
public sealed record GetAllUsersQuery : IRequest<List<UserResponse>>;


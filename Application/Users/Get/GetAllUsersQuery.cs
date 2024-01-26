using MediatR;

namespace Application.Users.Get;
public record GetAllUsersQuery : IRequest<List<UserResponse>>;


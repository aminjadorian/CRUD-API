using MediatR;

namespace Application.Users.Delete;
public sealed record DeleteUserCommand(Guid Id) : IRequest;

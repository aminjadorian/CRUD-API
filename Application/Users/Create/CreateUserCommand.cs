using Application.Behavior.Messaging;
using Domain.User;

namespace Application.Users.Create;
public record CreateUserCommand(string Email, Name name) : ICommand<Guid>;

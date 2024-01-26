using Application.Behavior.Messaging;
using Domain.User;

namespace Application.Users.Update;
public record UpdateUserCommand(Guid Id,Name name, string Email):ICommand;

public record UpdateUserRequest(Name name, string Email) : ICommand;


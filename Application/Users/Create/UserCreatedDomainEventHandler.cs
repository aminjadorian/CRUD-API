using Domain.Exceptions;
using Domain.User;
using MediatR;

namespace Application.Users.Create;

public sealed record UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;

    public UserCreatedDomainEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId);

        if(user is null)
            throw new UserNotFoundException(notification.UserId);

        sendEmail(user.Email);
    }
    private void sendEmail(string email)
    {
        //Send Email
    }
}

using Domain.Exceptions;
using Domain.User;
using MediatR;


namespace Application.Users.Get
{
    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            
            if (user is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            UserResponse response = new(user.Id, user.Email, user.Name.FirstName, user.Name.LastName);

            return response;
        }
    }
}

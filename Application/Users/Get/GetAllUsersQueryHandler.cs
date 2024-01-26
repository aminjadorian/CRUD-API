using Domain.Exceptions;
using Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Get
{
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            if (users is null)
                throw new NoUserFoundException();

            List<UserResponse> responses = new();
            foreach (var user in users)
            {
                responses.Add(new UserResponse(
                    user.Id,
                    user.Email,
                    user.Name.FirstName,
                    user.Name.LastName));
            }

            return responses;
        }
    }
}

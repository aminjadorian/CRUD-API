using Domain.Exceptions;
using Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isUserExist = await _userRepository.isUserExistAsync(request.Email);

            if(isUserExist )
                return Guid.Empty;

            var user = User.Create(request.name, request.Email);
            
            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}

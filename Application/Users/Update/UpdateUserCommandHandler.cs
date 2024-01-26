using Domain.Exceptions;
using Domain.User;
using MediatR;

namespace Application.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var fetchUser = await _userRepository.GetByIdAsync(request.Id);

            if (fetchUser is null)
                throw new UserNotFoundException(request.Id);

            fetchUser.Update(request.name,request.Email);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

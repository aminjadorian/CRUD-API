using Domain.Exceptions;
using Domain.User;
using MediatR;

namespace Application.Users.Delete
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var fetchUser = await _userRepository.GetByIdAsync(request.Id);

            if (fetchUser is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            
            _userRepository.Remove(fetchUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

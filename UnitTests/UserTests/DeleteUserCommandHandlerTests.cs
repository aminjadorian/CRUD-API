using Application;
using Application.Users.Delete;
using Domain.Exceptions;
using Domain.User;
using Moq;

namespace UnitTests.UserTests;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnUserNotFoundException_WhenIdIsNotExist()
    {
        _userRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult<User?>(null));

        var command = new DeleteUserCommand(Guid.NewGuid());

        var handler = new DeleteUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldBeExecuteDeleteOnce_WhenUserIdIsExist()
    {
        var user = User.Create(
                new Name("test", "test"),
                "test@gmail.com");

        _userRepositoryMock.Setup(
            x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);

        var command = new DeleteUserCommand(Guid.NewGuid());

        var handler = new DeleteUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);

        await handler.Handle(command, default);

        _userRepositoryMock.Verify(x =>
        x.Remove(It.IsAny<User>()), Times.Once());
    }
}

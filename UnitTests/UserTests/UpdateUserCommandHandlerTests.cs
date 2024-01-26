using Application;
using Application.Users.Update;
using Domain.Exceptions;
using Domain.User;
using Moq;

namespace UnitTests.UserTests;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public UpdateUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnNoUserExistException_WhenEmailIsNotExist()
    {
        //Arrange
        _userRepositoryMock.Setup(x =>
        x.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult<User?>(null));

        var command = new UpdateUserCommand(Guid.NewGuid(), new Name("FTest", "LTest"), "test@gmailc.com");

        var handler = new UpdateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldExecuteSaveChanges_WhenEmailIsExist()
    {
        //Arrange
        _userRepositoryMock.Setup(x =>
        x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(User.Create(new Name("FTest", "LTest"), "test@gmail.com"));

        var command = new UpdateUserCommand(Guid.NewGuid(), new Name("FTest", "LTest"), "test@gmailc.com");

        var handler = new UpdateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act
        await handler.Handle(command, default);

        //Assert
        _unitOfWorkMock.Verify(x =>
        x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

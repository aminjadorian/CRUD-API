using Application;
using Application.Users.Create;
using Domain.Exceptions;
using Domain.User;
using Moq;

namespace UnitTests.UserTests;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenEmailIsNotUnique()
    {
        //Arrange
        _userRepositoryMock.Setup(
            x => x.isUserExistAsync(It.IsAny<string>())).ReturnsAsync(true);

        var command = new CreateUserCommand("aminjdr95@gmail.com", new Name("Amin", "Jadorian"));

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act
        var result = await handler.Handle(command, default);
        //Assert
        Assert.Equal(result, Guid.Empty);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        //Arrange
        _userRepositoryMock.Setup(
           x => x.isUserExistAsync(It.IsAny<string>())).ReturnsAsync(true);

        var command = new CreateUserCommand("aminjdr95@gmail.com", new Name("Amin", "Jadorian"));

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act 
        await handler.Handle(command, default);

        //Assert
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldBeTypeOfGuid_WhenEmailIsUnique()
    {
        //Arrange
        _userRepositoryMock.Setup(
           x => x.isUserExistAsync(It.IsAny<string>())).ReturnsAsync(false);

        var command = new CreateUserCommand("aminjdr95@gmail.com", new Name("Amin", "Jadorian"));

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act 
        var result = await handler.Handle(command, default);

        //Assert
        Assert.IsType<Guid>(result);

    }

    [Fact]
    public async Task Handle_ShouldReturnUserGuidId_WhenEmailIsUnique()
    {
        //Arrange
        _userRepositoryMock.Setup(
           x => x.isUserExistAsync(It.IsAny<string>())).ReturnsAsync(false);

        var command = new CreateUserCommand("aminjdr95@gmail.com", new Name("Amin", "Jadorian"));

        var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act 
        var result = await handler.Handle(command, default);

        //Assert
        _userRepositoryMock.Verify(x => x.CreateAsync(It.Is<User>(m => m.Id == result)),
            Times.Once);

    }

}
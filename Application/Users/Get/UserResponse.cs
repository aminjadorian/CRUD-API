namespace Application.Users.Get;

public record UserResponse(
        Guid Id,
        string Email,
        string FirstName,
        string LastName);
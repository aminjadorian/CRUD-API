using Domain.Primitives;

namespace Domain.User;

public sealed record UserCreatedDomainEvent(Guid Id, Guid UserId) : IDomainEvent;

using MediatR;
using System.Collections.ObjectModel;

namespace Domain.Primitives;
public abstract class Entity : IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public List<IDomainEvent> DomainEvents => _domainEvents;
    public Guid Id { get; private init; }
    protected Entity(Guid id)
    {
        Id = id;
    }
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public static bool operator ==(Entity? left, Entity? right) => left is not null && right is not null && left.Equals(right);
    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
    public override int GetHashCode() => Id.GetHashCode() * 41;
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not Entity entity)
            return false;

        return entity.Id == Id;
    }
    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }
}

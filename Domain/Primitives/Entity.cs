namespace Domain.Primitives;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; protected set; } = default!;
}
namespace library.domain.Base;
public abstract class Entity<TEntityId> where TEntityId : class
{
    public TEntityId Id { get; protected set; }
}

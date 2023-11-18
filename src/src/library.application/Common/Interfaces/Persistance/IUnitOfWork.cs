namespace library.application.Common.Interfaces.Persistance;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct);
}

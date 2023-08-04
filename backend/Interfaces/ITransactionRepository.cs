using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Interfaces;

public interface ITransactionRepository
{
    public IDbContextTransaction BeginTransaction();

    public void CommitTransaction(IDbContextTransaction transaction);

    public void RollbackTransaction(IDbContextTransaction transaction);
}
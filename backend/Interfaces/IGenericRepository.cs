using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using backend.Dto;

namespace backend.Interfaces;

public interface IGenericRepository<T> where T : class
{
    ICollection<T> GetAll(PaginationParams @params,
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

    T GetById(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    bool Exist(Expression<Func<T, bool>> predicate);
    bool Delete(T value);
    bool UpdateEntity(T value);
    bool Save();

    List<T> GetAll(PaginationParams? @params,
        Func<IQueryable<T>, IQueryable<T>>? includeFunc);

    T GetById2(Func<IQueryable<T>, IQueryable<T>>? includeFunc);
}
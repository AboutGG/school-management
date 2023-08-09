using System.Linq.Expressions;
using backend.Dto;

namespace backend.Interfaces;

public interface IGenericRepository<T> where T : class
{
    ICollection<T> GetAll(PaginationParams @params, Expression<Func<T,bool>> predicate, Func<T, object> order, params Expression<Func<T, object>>[] includes);
    
}
using System.Linq.Expressions;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

/// <summary>
/// Genereic repository which have some essential crowd to use for all Entities
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    #region Attributes

    private readonly SchoolContext _context;
    private readonly DbSet<T> _entities;

    #endregion

    #region Costrutor

    public GenericRepository(SchoolContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    #endregion

    /// <summary>
    /// This function return all the element of a table taking a determinate condition
    /// </summary>
    /// <param name="params"></param>
    /// <param name="predicate"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public ICollection<T> GetAll(PaginationParams @params, Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes) //i pass the lambda function, ex: p=> p.id == id
    {
        var query = _entities
            .Where(predicate)
            .Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage);

        if (@params.Role != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.ToList();
    }
}
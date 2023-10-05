using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
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

    #region Methods

    #region GetAll

    /// <summary> This function return all the element of a table taking a determinate condition </summary>
    /// <param name="params"> The params are used to do Skip and Take, the order and order's type, the page number and more </param>
    /// <param name="predicate"> Used to do a condition in a search or more.</param>
    /// <param name="includes"> Used to includes the reference object of another table. </param>
    /// <returns>All users using the params, predicate and includes</returns>

    public List<T> GetAll(PaginationParams? @params,
        Expression<Func<T, bool>> predicate, //Predicate ex:  t => t.Id == Id
        params Expression<Func<T, object>>[] includes //Include ex:  t => t.Id<
    )
    {
        var query = _entities
            .Where(predicate);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        //@params.Order default value: Name, @params.OderType default value: asc
        query = query.OrderBy(
            $"{@params.Order} {@params.OrderType}"); // Order to Student.Registry.{params order} and Teacher.Registry{params order}

        //@params.Page default value: 1, @params.ItemsPerPage default value: 10
        return query.Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage).ToList();
    }

    #endregion

    #region Get All using IQueryable

    public List<T> GetAllUsingIQueryable(@PaginationParams? @params,
        Func<IQueryable<T>, IQueryable<T>>? queryFunc,
        out int total
    )
    {
        var query = _entities.AsQueryable();

        if (queryFunc != null)
        {
            query = queryFunc.Invoke(query);
        }

        if (query != null)
        {
            total = query.Count();
        }
        else
        {
            total = 0;
        }

        if (@params != null)
        {
            query = query.OrderBy(
                $"{@params.Order} {@params.OrderType}"); // Order to Student.Registry.{params order} and Teacher.Registry{params order}

            //@params.Page default value: 1, @params.ItemsPerPage default value: 10
            query = query.Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage);
        }

        return query.ToList();
    }

    #endregion

    #region GetById

    /// <summary> Having a predicate i search a record.  </summary>
    /// <param name="predicate">Used to do a condition in a search or more. </param>
    /// <param name="includes"> Used to includes the reference object of another table. </param>
    /// <returns>Users detail: User, if its Student or teacher and his registry</returns>
    public T GetById(Expression<Func<T, bool>> predicate, //Predicate ex: u => u.Id == Id.
        params Expression<Func<T, object>>[] includes) //Include ex:  t => t.Id.
    {
        var query = _entities.Where(predicate);
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.FirstOrDefault();
    }
    
    #endregion

    #region GetById using IQueryable

    public T GetByIdUsingIQueryable(Func<IQueryable<T>, IQueryable<T>>? queryFunc) //Include ex:  t => t.Id.
    {
        var query = _entities.AsQueryable();

        if (queryFunc != null)
        {
            query = queryFunc.Invoke(query);
        }

        return query.FirstOrDefault();
    }

    #endregion

    #region Exists

    /// <summary>
    /// Check if a record exists taking a predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>True if exists, False if not</returns>
    public bool Exist(Expression<Func<T, bool>> predicate) //predicate ex: u => u.Id == Id
    {
        return _entities.Any(predicate);
    }

    #endregion

    #region Update

    public bool UpdateEntity(T value)
    {
        _context.Update(value);
        return Save();
    }

    #endregion

    #region Delete

    /// <summary> Delete an user taking the Id </summary>;
    /// <param name="Id">Is the id of an user</param>
    /// <returns>A boolean which indicate if was deleted</returns>
    public bool Delete(T value)
    {
        _context.Remove(value);
        return Save();
    }

    #endregion

    #region Create

    public bool Create(T value)
        {
            _context.Add(value);
            return Save();
        }

    #endregion
    
    #region Save

    public bool Save()
    {
        if (_context.SaveChanges() > 0)
        {
            return true;
        }

        throw new Exception();
    }

    #endregion

    #endregion

}
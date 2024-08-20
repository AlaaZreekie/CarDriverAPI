using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Cars;

public interface IRepository<T> where T : class
{
    public IQueryable<T>? GetAll(params Expression<Func<T, object>>[] navigationProperties);
    public T? GetById(int id, params Expression<Func<T, object>>[] navigationProperties);
    public T? Create(T t, params Expression<Func<T, object>>[] navigationProperties);
    public T? Update(T t, params Expression<Func<T, object>>[] navigationProperties);
    public T? Delete(T t, params Expression<Func<T, object>>[] navigationProperties);

}
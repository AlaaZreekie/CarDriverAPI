using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Data.Cars;
using System.Reflection.Metadata;
using Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Cars;

public class Repository<T>(ApplicationDBContext context) : IRepository<T> where T : class,ITEntity  //RepositoryCar
{
    private readonly ApplicationDBContext _context = context;

    private readonly DbSet<T> _entities = context.Set<T>();
    public IQueryable<T> Table => _entities;

    
    public IQueryable<T>? GetAll(params Expression<Func<T, object>>[] navigationProperties)               
    {
        IQueryable<T> query = _entities;

        if (navigationProperties != null)
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                query = query.Include<T, object>(navigationProperty);
        return query;
    }

    //Get car by id
    public T? GetById(int id, params Expression<Func<T, object>>[] navigationProperties)                      
    {
        IQueryable<T> query = _entities;

        if (navigationProperties != null)
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                query = query.Include<T, object>(navigationProperty);
        return query.FirstOrDefault(c => c.Id == id);
    }
    public T? Create(T t, params Expression<Func<T, object>>[] navigationProperties)                       
    {           
          _entities.Add(t);        
          _context.SaveChanges();
          return t; 
    }
    
   public T? Update(T t, params Expression<Func<T, object>>[] navigationProperties)             
    {
        _entities.Update(t);        
        _context.SaveChanges();
        return t;
    }
     public T? Delete(T t, params Expression<Func<T, object>>[] navigationProperties)                       
    {
        _entities.Remove(t);
        _context.SaveChanges();
        return t;
        
    }
}
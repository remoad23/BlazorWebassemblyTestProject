using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CheckListLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CheckListLibrary
{
    public abstract class GenericRepository<T,DB> : IGenericRepository<T> where DB : class 
        where T : class
    {
        private readonly DbContext _context;
        
        public GenericRepository(DB context)
        {
            _context = context as DbContext;
        }
        
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll(bool withEntity = false,Expression<Func<Object, Object>> expression = null)
        {
            var list =_context.Set<T>();
            if (withEntity)
                list.Include(expression);
            return list;
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
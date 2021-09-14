using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        
        public Task<IEnumerable<T>>  GetAll(string entityToInclude = "")
        {
            if (!entityToInclude.Equals(""))
            {
                return Task.FromResult(_context.Set<T>().Include("Entries").AsEnumerable());
            }
            else 
            {
                return Task.FromResult(_context.Set<T>().AsEnumerable());
            }
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
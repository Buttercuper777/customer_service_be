using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomersService.Core;
using CustomersService.Core.Models;

namespace CustomersService.DataAccess
{
    public class DbRepository : IDbRepository
    {
        private readonly DataContext _context;

        public DbRepository(DataContext context)
        {
            _context = context;
        }
        
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return _context.Set<T>().Where(selector).AsQueryable();
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<Guid> Add<T>(T newEntity) where T : class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;    
        }

        public async Task Delete<T>(Guid id) where T : class, IEntity
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null) _context.Set<T>().Remove(entity);
        }

        public Task Update<T>(T entity) where T : class, IEntity
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
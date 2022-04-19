using Microsoft.EntityFrameworkCore;
using Social_Media.Data;
using Social_Media.Data.DataModels.Entities;
using Social_Media.Data.DataModels.Entities.Interfaces;
using Social_Media.Data.DataModels.Entities_Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.EntityFramework
{
    public class ContextEntityFramework : IRepositoryEntityFramework
    {
        private ApplicationContext _context;

        public ContextEntityFramework(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task CreateAsync<T>(T entity) where T : class, IEntity
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChagesAsync();
            
        }

        public async Task RemovetAsync<T>(T entity) where T : class, IEntity
        {
            _context.Set<T>().Remove(entity);
            await SaveChagesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class, IEntity
        {
            _context.Set<T>().Update(entity);
            await SaveChagesAsync();
        }

        private async Task<int> SaveChagesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

using Social_Media.Data.Models.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.EntityFramework
{
    public interface IRepositoryEntityFramework
    {
        IQueryable<T> GetAll<T>() where T : class, IEntity;
        Task CreateAsync<T>(T entity) where T : class, IEntity;
        Task RemovetAsync<T>(T entity) where T : class, IEntity;
        Task UpdateAsync<T>(T entity) where T : class, IEntity;
    }
}

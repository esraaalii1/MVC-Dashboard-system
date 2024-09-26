using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Data;
using DemoDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Repositories
{
    public class GenericRepository<TEntity> :IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dataContext;

        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<TEntity?> GetAsync(int id) => await _dataContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dataContext.Set<TEntity>().ToListAsync();

        public void Update(TEntity entitiy) => _dataContext.Set<TEntity>().Update(entitiy);

        public async Task AddAsync(TEntity entitiy) => await _dataContext.Set<TEntity>().AddAsync(entitiy);

        public void Delete(TEntity entitiy) => _dataContext.Set<TEntity>().Remove(entitiy);
    }
}

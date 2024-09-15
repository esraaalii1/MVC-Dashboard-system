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
        public TEntity? Get(int id) => _dataContext.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => _dataContext.Set<TEntity>().ToList();

        public void Update(TEntity entitiy) => _dataContext.Set<TEntity>().Update(entitiy);

        public void Create(TEntity entitiy) => _dataContext.Set<TEntity>().Add(entitiy);

        public void Delete(TEntity entitiy) => _dataContext.Set<TEntity>().Remove(entitiy);
    }
}

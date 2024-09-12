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

        public int Update(TEntity entitiy)
        {
            _dataContext.Set<TEntity>().Update(entitiy);
            return _dataContext.SaveChanges();
        }

        public int Create(TEntity entitiy)
        {
            _dataContext.Set<TEntity>().Add(entitiy);
            return _dataContext.SaveChanges();
        }

        public int Delete(TEntity entitiy)
        {
            _dataContext.Set<TEntity>().Remove(entitiy);
            return _dataContext.SaveChanges();
        }
    }
}

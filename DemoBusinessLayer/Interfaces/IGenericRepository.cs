using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Interfaces
{
    public   interface IGenericRepository<TEntity>
    {
        Task AddAsync(TEntity entitiy);
        void Delete(TEntity entitiy);
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entitiy);
    }
}

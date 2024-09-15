using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Interfaces
{
    public   interface IGenericRepository<TEntity>
    {
        void Create(TEntity entitiy);
        void Delete(TEntity entitiy);
        TEntity? Get(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entitiy);
    }
}

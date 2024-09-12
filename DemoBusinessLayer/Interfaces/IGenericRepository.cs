using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Interfaces
{
    public   interface IGenericRepository<TEntity>
    {
         int Create(TEntity entitiy);
        int Delete(TEntity entitiy);
        TEntity? Get(int id);
        IEnumerable<TEntity> GetAll();
        int Update(TEntity entitiy);
    }
}

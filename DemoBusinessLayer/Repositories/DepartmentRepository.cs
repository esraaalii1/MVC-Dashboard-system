using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Data;
using DemoDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository

    {
        public DepartmentRepository(DataContext dataContext) : base(dataContext)
        {

        }

    }
}

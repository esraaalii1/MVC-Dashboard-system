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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(DataContext dataContext):base(dataContext)
        {
            
        }

        public IEnumerable<Employee> Get(string Address)
        {
           return _dataContext.Set<Employee>().Where(e=>e.Address.ToLower() == Address.ToLower()).ToList();    
        }
    }
}

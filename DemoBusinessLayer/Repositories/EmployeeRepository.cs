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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(DataContext dataContext):base(dataContext)
        {
            
        }
        public async Task<IEnumerable<Employee>> GetAllAsync(string Name)
        {
           return await _dataContext.Set<Employee>().Where(e=>e.Name.ToLower().Contains( Name.ToLower())).Include(e=>e.Department).ToListAsync();    
        }

        public async Task<IEnumerable<Employee>> GetWithDepartmentAsync()
        {
            return await _dataContext.Employees.Include(e => e.Department).ToListAsync();
        }
    }
}

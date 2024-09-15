using DemoDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        public IEnumerable<Employee> SearchByName(string Name);
        public IEnumerable<Employee> GetWithDepartment();


    }
}

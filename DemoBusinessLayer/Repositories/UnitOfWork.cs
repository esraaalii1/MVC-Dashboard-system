using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DataContext _dataContext;

        public UnitOfWork( DataContext dataContext)
        {
            _employeeRepository = new EmployeeRepository(dataContext);
            _departmentRepository = new DepartmentRepository(dataContext);
            _dataContext = dataContext;
        }

        public IEmployeeRepository Employees => _employeeRepository;

        public IDepartmentRepository Departments => _departmentRepository;

        public int SaveChanges() => _dataContext.SaveChanges();
    }
}

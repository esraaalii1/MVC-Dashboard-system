using DemoDataAccessLayer.Data;
using DemoDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBusinessLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository

    {
        private readonly DataContext _dataContext;
        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Department? Get(int id) => _dataContext.Departments.Find(id);

        public IEnumerable<Department> GetAll() => _dataContext.Departments.ToList();

        public int Update(Department entitiy)
        {
            _dataContext.Departments.Update(entitiy);
            return _dataContext.SaveChanges();
        }

        public int Create(Department entitiy)
        {
            _dataContext.Departments.Add(entitiy);
            return _dataContext.SaveChanges();
        }

        public int Delete(Department entitiy)
        {
            _dataContext.Departments.Remove(entitiy);
            return _dataContext.SaveChanges();
        }
    }
}

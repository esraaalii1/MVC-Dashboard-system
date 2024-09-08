using DemoDataAccessLayer.Models;

namespace DemoBusinessLayer.Repositories
{
    public interface IDepartmentRepository
    {
        int Create(Department entitiy);
        int Delete(Department entitiy);
        Department? Get(int id);
        IEnumerable<Department> GetAll();
        int Update(Department entitiy);
    }
}
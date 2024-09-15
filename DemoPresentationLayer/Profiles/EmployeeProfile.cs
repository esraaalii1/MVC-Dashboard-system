global using AutoMapper;
using DemoDataAccessLayer.Models;

namespace DemoPresentationLayer.Profiles
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeVM,Employee>().ReverseMap();
        }

    }
}

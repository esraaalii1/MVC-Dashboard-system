using DemoDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccessLayer.Data
{
    public class DataContext : DbContext
    {
      
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //}
        public DbSet<Department> Departments { get; set; }
    }
}

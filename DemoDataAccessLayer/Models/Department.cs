using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Range(0,500)]
        public int Code { get; set; }
        [Required(ErrorMessage ="NAme is required !!")]
        public string Name { get; set; }
        [Display(Name ="Created At")]
        public DateTime DateOfCreation { get; set; }

    }
}

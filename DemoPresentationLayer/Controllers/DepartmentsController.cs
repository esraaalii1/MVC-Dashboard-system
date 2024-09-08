using DemoBusinessLayer.Repositories;
using DemoDataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var departments=_departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid) return View(department);
                _departmentRepository.Create(department);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if(!id.HasValue)return BadRequest();
            var department=_departmentRepository.Get(id.Value);

            if (id==null) return NotFound();
            return View(department);
        }
       
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if (id == null) return NotFound();
            return View(department);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id, Department department)
        {
            if(id!=department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);

        }
        public IActionResult Delete(int? id)
        {
            
                if (!id.HasValue) return BadRequest();
                var department = _departmentRepository.Get(id.Value);

                if (id == null) return NotFound();
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            
               
                    _departmentRepository.Delete(department);
                    return RedirectToAction(nameof(Index));

                

        }

    }
}

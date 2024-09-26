using DemoBusinessLayer.Interfaces;
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
            var departments=_departmentRepository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(!ModelState.IsValid) return View(department);
               await _departmentRepository.AddAsync(department);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id) => await DepartmentControllerHandler(id, nameof(Details));


        public async Task<IActionResult> Edit(int? id) => await DepartmentControllerHandler(id, nameof(Edit));
       
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
        public async Task<IActionResult> Delete(int? id) => await DepartmentControllerHandler(id,nameof(Delete));

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete([FromRoute] int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = await _departmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound();
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(department);
        }
        private async Task<IActionResult> DepartmentControllerHandler(int? id,string ViewName)
        {
            if (!id.HasValue) return BadRequest();
            var department = await _departmentRepository.GetAsync(id.Value);

            if (id == null) return NotFound();
            return View(ViewName,department);
        }


    }
}

using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeesController(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }

        public IActionResult Index()
        {
            var Employees = _EmployeeRepository.GetAll();
            return View(Employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
            _EmployeeRepository.Create(employee);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));


        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _EmployeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);

        }
        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete([FromRoute] int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _EmployeeRepository.Get(id.Value);

            if (employee is null) return NotFound();
            try
            {
                _EmployeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(employee);
        }
        private IActionResult EmployeeControllerHandler(int? id, string ViewName)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _EmployeeRepository.Get(id.Value);

            if (id == null) return NotFound();
            return View(ViewName, employee);
        }
    }
}

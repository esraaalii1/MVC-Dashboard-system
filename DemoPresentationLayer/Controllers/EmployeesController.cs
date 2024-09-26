global using DemoPresentationLayer.ViewModels;
using AutoMapper;
using DemoBusinessLayer.Interfaces;
using DemoBusinessLayer.Repositories;
using DemoDataAccessLayer.Models;
using DemoPresentationLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DemoPresentationLayer.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public EmployeesController(IUnitOfWork UnitOfWork,IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _Mapper = Mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? SearchValue)
        {
            //ViewData["message"] = new Employee { Name = "salma" };
            //ViewData["message"] ="hello from view data;
            //ViewBag.Message = new Employee { Name = "salma" };
            var Employees=Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(SearchValue))
            {
                 Employees =await _unitOfWork.Employees.GetWithDepartmentAsync();
            }
            else Employees= await _unitOfWork.Employees.GetAllAsync(SearchValue);
            var employeeVM = _Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeVM>>(Employees);

            return View(employeeVM);
        }
        public async Task<IActionResult> Create()
        {   
            var departments =await _unitOfWork.Departments.GetAllAsync();
            SelectList listItem = new SelectList(departments,"Id","Name");
            ViewBag.Departments = listItem;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid) return View(employeeVM);//server side validation
            if (employeeVM.Image is not null)
                employeeVM.ImageName = await DocumentSetting.UploadFileAsync(employeeVM.Image, "Images");
            var employee = _Mapper.Map<EmployeeVM, Employee>(employeeVM);
            await _unitOfWork.Employees.AddAsync(employee);
            if ( await _unitOfWork.SaveChangesAsync()>0)
                TempData["Message"] = "Employee created successfully";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id) => await EmployeeControllerHandler(id, nameof(Details));


        public async Task<IActionResult> Edit(int? id) =>await EmployeeControllerHandler(id, nameof(Edit));

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeVM employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName =await DocumentSetting.UploadFileAsync(employeeVM.Image, "Images");
                    var employee=_Mapper.Map<Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (await _unitOfWork.SaveChangesAsync() > 0)
                    
                        TempData["Message"] = "Employee updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVM);

        }
        public async Task<IActionResult> Delete(int? id) => await EmployeeControllerHandler(id, nameof(Delete));

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete([FromRoute] int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = await _unitOfWork.Employees.GetAsync(id.Value);

            if (employee is null) return NotFound();
            try
            {
                _unitOfWork.Employees.Delete(employee);
                if(await _unitOfWork.SaveChangesAsync()>0 && employee.ImageName is not null)
                    DocumentSetting.DeleteFile("Images",employee.ImageName);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(employee);
        }
        private async Task<IActionResult> EmployeeControllerHandler(int? id, string ViewName)
        {
            if (ViewName == nameof(Edit))
            {
                var departments =await _unitOfWork.Departments.GetAllAsync();
                SelectList listItem = new SelectList(departments, "Id", "Name");
                ViewBag.Departments = listItem;
            }
            if (!id.HasValue) return BadRequest();
            var employee = await _unitOfWork.Employees.GetAsync(id.Value);

            if (id == null) return NotFound();
            var employeeVM=_Mapper.Map<EmployeeVM>(employee);
            return View(ViewName, employeeVM);
        }
    }
}

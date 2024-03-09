using Assingment.Models;
using Assingment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assingment.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Create(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool result = _employeeRepository.Update(employee);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            bool result = _employeeRepository.Delete(employee.Id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
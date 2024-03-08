using Assingment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assingment.Models;

namespace Assingment.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly AppointmentRepository _repository;

        public AppointmentController(AppointmentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var appointments = _repository.GetAll();
            return View(appointments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _repository.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _repository.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Delete(Appointment model)
        {
            _repository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
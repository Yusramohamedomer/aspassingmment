using Assingment.Data;
using Assingment.Models;
using Assingment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Assingment.Controllers
{

    [Authorize]
    public class PatientController : Controller

    {

        private readonly DentalRepository _repository;

        public PatientController()
        {
            _repository = new DentalRepository();
        }

        public IActionResult Index()
        {
            var data = _repository.GetLastTop50();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient model)
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
            var patient = _repository.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult Edit(Patient model)
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
            var patient = _repository.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult Delete(Patient model)
        {
            _repository.Delete(model.Id);
            return RedirectToAction("Index");
        }
    }
}
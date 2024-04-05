﻿using Microsoft.AspNetCore.Mvc;
using Movie.BL.Models;
using Movie.BL.Services;
using Movie.DAL.Extensions;

namespace Movie.UI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoriesService _service;
        public CategoriesController(CategoriesService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoriesDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(newCategory); 
            }

            try
            {
                var createdCategory = await _service.CreateAsync(newCategory);
                TempData["SuccessMessage"] = "Категорія успішно створено!";
                return RedirectToAction("Index");
            }
            catch (DuplicateItemException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (ServerErrorException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(newCategory);
        }
    }
}
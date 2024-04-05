using Microsoft.AspNetCore.Mvc;
using Movie.BL.Models;
using Movie.BL.Services;
using Movie.DAL.Extensions;

namespace Movie.UI.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmService _service;
        public FilmsController(FilmService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FilmsDTO newFilms)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return View(newFilms);
            }

            try
            {
                var createdFilms = await _service.CreateAsync(newFilms);
                TempData["SuccessMessage"] = "Фільм успішно створено!";
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

            return View(newFilms);
        }
    }
}

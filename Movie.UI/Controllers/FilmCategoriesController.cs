using Microsoft.AspNetCore.Mvc;
using Movie.BL.Models;
using Movie.BL.Services;
using Movie.DAL.Extensions;

namespace Movie.UI.Controllers
{
    public class FilmCategoriesController : Controller
    {
        private readonly FilmCategoriesService _service;
        public FilmCategoriesController(FilmCategoriesService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get and Post method inCreate
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FilmCategoriesDTO newFilms)
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
                TempData["SuccessMessage"] = "FilmCategories успішно створено!";
                return RedirectToAction("Index");
            }
            catch (DuplicateItemException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View(newFilms);
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<ICollection<FilmCategoriesDTO>>> GetAll()
        {
            try
            {
                var films = await _service.GetAllAsync();
                return View(films);
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult<FilmCategoriesDTO>> GetById(int id)
        {
            try
            {
                var films = await _service.GetByIdAsync(id);
                return View(films);
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        #region Http Get & Post method Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var films = await _service.GetByIdAsync(id);
                if (films == null || films.Id != id)
                {
                    TempData["ErrorMessage"] = "FilmCategories з таким ідентифікатором не знайдено";
                    return RedirectToAction("Index");
                }
                return View(films);
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult<FilmCategoriesDTO>> Update(FilmCategoriesDTO update)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                return RedirectToAction("Index");
            }

            try
            {
                await _service.UpdateAsync(update);
                TempData["SuccessMessage"] = "FilmCategories успішно оновлено!";
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (DuplicateItemException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Http Get & Post method Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var films = await _service.GetByIdAsync(id);
                return View(films);
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "FilmCategories успішно видалено!";
            }
            catch (InvalidIdException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ServerErrorException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}

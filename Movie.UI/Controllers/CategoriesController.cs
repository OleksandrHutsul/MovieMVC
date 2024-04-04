using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> CreateAsync(CategoriesDTO newCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(newCategory);
            }

            try
            {
                var createdCategory = await _service.CreateAsync(newCategory);
                return RedirectToAction("Details", new { id = createdCategory.Id });
            }
            catch (DuplicateItemException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (ServerErrorException ex)
            {
            }

            return View(newCategory);
        }
    }
}

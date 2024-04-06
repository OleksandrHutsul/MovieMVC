using Microsoft.AspNetCore.Mvc;
using Movie.BL.Models;
using Movie.BL.Services;
using Movie.DAL.Extensions;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmCategoriesController: ControllerBase
    {
        private readonly FilmCategoriesService _service;
        public FilmCategoriesController(FilmCategoriesService service) => _service = service;

        [HttpGet("GetAll")]
        public async Task<ActionResult<ICollection<FilmCategoriesDTO>>> GetAllAsync()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<FilmCategoriesDTO>> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (InvalidIdException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<FilmCategoriesDTO>> CreateAsync([FromBody] FilmCategoriesDTO newEntity)
        {
            try
            {
                return (!ModelState.IsValid) ?
                        BadRequest() :
                        Ok(await _service.CreateAsync(newEntity));
            }
            catch (DuplicateItemException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("Update")]
        public async Task<ActionResult<FilmCategoriesDTO>> UpdateAsync([FromBody] FilmCategoriesDTO update)
        {
            try
            {
                return (!ModelState.IsValid) ?
                    BadRequest() :
                    Ok(await _service.UpdateAsync(update));
            }
            catch (InvalidIdException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (DuplicateItemException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidIdException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

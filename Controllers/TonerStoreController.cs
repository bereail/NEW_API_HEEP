using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toner_store.Model.DTO;
using toner_store.Models;

namespace toner_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TonerStoreController : BaseController // Hereda de BaseController
    {
        public TonerStoreController(tonerStoreContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        [Route("toners")]
        public IActionResult GetToners()
        {
            try
            {
                var listToners = _dbContext.Toner
                    .Select(t => new TonerDTO { Id = t.Id, Name = t.Name, Cant = t.Cant })
                    .ToList();

                if (listToners != null && listToners.Any())
                {
                    return Ok(listToners);
                }

                return Ok("No toners found in the database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("toners")]
        public IActionResult CreateToner([FromBody] TonerDTO tonerDto)
        {
            try
            {
                // Validar que los datos recibidos sean correctos, por ejemplo:
                if (string.IsNullOrEmpty(tonerDto.Name) || tonerDto.Cant <= 0)
                {
                    return BadRequest("Invalid toner data.");
                }

                // Crear un objeto Toner a partir de los datos del TonerDTO
                var toner = new Toner
                {
                    Name = tonerDto.Name,
                    Cant = tonerDto.Cant
                };

                // Agregar el nuevo toner a la base de datos y guardar los cambios
                _dbContext.Toner.Add(toner);
                _dbContext.SaveChanges();

                // Devolver una respuesta indicando que la operación se realizó con éxito
                return Ok("Toner created successfully.");
            }
            catch (Exception ex)
            {
                // En caso de error, devolver un mensaje con el detalle del error.
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("toners/{id}")]
        public IActionResult GetTonerById(int id)
        {
            try
            {
                var toner = _dbContext.Toner.Find(id);

                if (toner == null)
                {
                    return NotFound("Toner not found");
                }

                var tonerDto = new TonerDTO
                {
                    Id = toner.Id,
                    Name = toner.Name,
                    Cant = toner.Cant
                };

                return Ok(tonerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("toners/{id}")]
        public IActionResult UpdateToner(int id, [FromBody] TonerDTO updatedTonerDto)
        {
            try
            {
                var toner = _dbContext.Toner.Find(id);

                if (toner == null)
                {
                    return NotFound("Toner not found");
                }

                // Validar que los datos recibidos sean correctos, por ejemplo:
                if (string.IsNullOrEmpty(updatedTonerDto.Name) || updatedTonerDto.Cant <= 0)
                {
                    return BadRequest("Invalid toner data.");
                }

                // Actualizar los datos del toner
                toner.Name = updatedTonerDto.Name;
                toner.Cant = updatedTonerDto.Cant;

                _dbContext.SaveChanges();

                return Ok("Toner updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("toners/{id}")]
        public IActionResult DeleteToner(int id)
        {
            try
            {
                var toner = _dbContext.Toner.Find(id);

                if (toner == null)
                {
                    return NotFound("Toner not found");
                }

                _dbContext.Toner.Remove(toner);
                _dbContext.SaveChanges();

                return Ok("Toner deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

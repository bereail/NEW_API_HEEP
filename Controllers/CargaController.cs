using Microsoft.AspNetCore.Mvc;
using toner_store.Model.DTO;
using toner_store.Models;
using System;
using toner_store.Model.ViewModel;

namespace toner_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargaController : BaseController
    {

        public CargaController(tonerStoreContext dbContext) : base(dbContext)
        {
        }

        // GET: Carga/Details/5
        [HttpGet]
        [Route("carga/{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                // Buscar la carga en la base de datos por su ID
                var carga = _dbContext.Carga.Find(id);

                if (carga == null)
                {
                    return NotFound("Carga not found.");
                }

                // Crear un objeto DTO para enviar los detalles de la carga
                var cargaDTO = new CargaDTO
                {
                    Id = carga.Id,
                    IdUser = carga.IdUser,
                    IdToner = carga.IdToner,
                    IdService = carga.IdService,
                    Cant = carga.Cant,
                    CargaAt = carga.CargaAt
                };

                // Devolver una respuesta exitosa con los detalles de la carga
                return Ok(cargaDTO);
            }
            catch (Exception ex)
            {
                // En caso de error, devolver una respuesta con el mensaje de error.
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }


        // POST: Carga/Create
        [HttpPost]
        [Route("carga")]
        public IActionResult Create(CargaDTO cargaDTO)
        {
            try
            {
                // Validar que los datos recibidos sean correctos
                if (cargaDTO.IdUser == null || cargaDTO.IdToner == null || cargaDTO.IdService == null || cargaDTO.Cant == null || cargaDTO.CargaAt == null)
                {
                    return BadRequest("Invalid carga data.");
                }

                // Si los datos son válidos, crear un objeto Carga a partir de los datos del CargaDTO
                var carga = new Carga
                {
                    IdUser = cargaDTO.IdUser.Value,
                    IdToner = cargaDTO.IdToner.Value,
                    IdService = cargaDTO.IdService.Value,
                    Cant = cargaDTO.Cant.Value,
                    CargaAt = cargaDTO.CargaAt.Value
                };

                // Agregar la nueva carga a la base de datos y guardar los cambios
                _dbContext.Carga.Add(carga);
                _dbContext.SaveChanges();

                // Redirigir a la acción Index o a otra vista de éxito si es necesario
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // En caso de error, devolver una vista con el mensaje de error.
                var errorViewModel = new ErrorViewModel { Message = ex.Message };
                return View("Error", errorViewModel);
            }
        }

        // PUT: Carga/Update/5
        [HttpPut]
        [Route("carga/{id}")]
        public IActionResult Update(int id, CargaDTO cargaDTO)
        {
            try
            {
                // Buscar la carga en la base de datos por su ID
                var carga = _dbContext.Carga.Find(id);

                if (carga == null)
                {
                    return NotFound("Carga not found.");
                }

                // Actualizar los datos de la carga con los valores del DTO
                carga.IdUser = cargaDTO.IdUser.Value;
                carga.IdToner = cargaDTO.IdToner.Value;
                carga.IdService = cargaDTO.IdService.Value;
                carga.Cant = cargaDTO.Cant.Value;
                carga.CargaAt = cargaDTO.CargaAt.Value;

                // Guardar los cambios en la base de datos
                _dbContext.SaveChanges();

                // Devolver una respuesta exitosa con los detalles de la carga actualizada
                return Ok("Carga updated successfully.");
            }
            catch (Exception ex)
            {
                // En caso de error, devolver una respuesta con el mensaje de error.
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        // DELETE: Carga/Delete/5
        [HttpDelete]
        [Route("carga/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Buscar la carga en la base de datos por su ID
                var carga = _dbContext.Carga.Find(id);

                if (carga == null)
                {
                    return NotFound("Carga not found.");
                }

                // Eliminar la carga de la base de datos y guardar los cambios
                _dbContext.Carga.Remove(carga);
                _dbContext.SaveChanges();

                // Devolver una respuesta exitosa con el mensaje de éxito
                return Ok("Carga deleted successfully.");
            }
            catch (Exception ex)
            {
                // En caso de error, devolver una respuesta con el mensaje de error.
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        private IActionResult View(string v, ErrorViewModel errorViewModel)
        {
            throw new NotImplementedException();
        }
    }
}

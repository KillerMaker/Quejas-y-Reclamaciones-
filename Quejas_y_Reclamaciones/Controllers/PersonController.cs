using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quejas_y_Reclamaciones.Models;

namespace Quejas_y_Reclamaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CPerson person) 
        {
            if (person.user == null)
                return BadRequest("Informacion de usuario Insuficiente");
            else
                return Ok(await person.Insert());
        } 

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("Informacion de busqueda Insuficiente Falta Id");
            else
                return Ok(await CPerson.Delete(id.Value));

        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> put(CPerson person)
        {
            if (!person.id.HasValue)
                return BadRequest("Informacion de busqueda Insuficiente Falta Id");
            else
                return Ok(await person.Update());
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> get(string searchString)
        {
            if (CPerson.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CPerson.Select(searchString));
        }

    }
}

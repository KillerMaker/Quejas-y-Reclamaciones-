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
        public async Task<IActionResult> Post(CPerson obj) 
        {
            if (obj.user == null)
                return BadRequest("Informacion de usuario Insuficiente");
            else
                return Ok(await obj.Insert());
        } 

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CPerson.Delete(id));
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CPerson obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion de busqueda Insuficiente Falta Id");
            else
                return Ok(await obj.Update());
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> Get(string searchString)
        {
            if (CPerson.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CPerson.Select(searchString));
        }

    }
}

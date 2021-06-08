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
        public IActionResult Post(CPerson person) 
        {
            if (person.user == null)
                return BadRequest("Informacion de usuario Insuficiente");
            else
                return Ok(person.Insert());
        } 

        [HttpDelete("Eliminar")]
        public IActionResult delete(CPerson person)
        {
            if (!person.id.HasValue)
                return BadRequest("Informacion de busqueda Insuficiente Falta Id");
            else
                return Ok(person.Delete());

        }

        [HttpPut("Actualizar")]
        public IActionResult put(CPerson person)
        {
            if (!person.id.HasValue)
                return BadRequest("Informacion de busqueda Insuficiente Falta Id");
            else
                return Ok(person.Update());
        }

        [HttpGet("Mostrar/{searchString?}")]
        public IActionResult get(string searchString)
        {
            if (CPerson.Select(searchString).Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(CPerson.Select(searchString));
        }

    }
}

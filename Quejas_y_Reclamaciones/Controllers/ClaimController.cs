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
    public class ClaimController : ControllerBase,IController<CClaim>
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CClaim obj)
        {
            if (obj.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(await obj.Insert());
        }
        
        [HttpGet("Mostrar")]
        public async Task<IActionResult> Get(string searchString)
        {
            searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO=3";

            if (CClaim.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CClaim.Select(searchString));
        }
        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString = $"WHERE ID_PERSONA={id} AND ID_ESTADO!=3";

            if (CClaim.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CClaim.Select(searchString));
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CClaim obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(await obj.Update());
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           return Ok(await CClaim.Delete(id));
        }
    }
}

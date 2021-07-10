using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quejas_y_Reclamaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase,IController<CAnswer>
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CAnswer obj)
        {
            if (obj.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(await obj.Insert());
        }

        [HttpGet("Mostrar")]
        public async Task<IActionResult> Get(string searchString)
        {
           // searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO=3";

            if (CAnswer.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CAnswer.Select(searchString));
        }
        [HttpGet("MostrarQueja/{id:int}")]
        public async Task<IActionResult> GetClaim(int id)
        {
            // searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO=3";
            string searchString = $"WHERE ID_QUEJA > 0 AND ID_PERSONA_QUEJA = {id}";

            if (CAnswer.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CAnswer.Select(searchString));
        }

        [HttpGet("MostrarReclamacion/{id:int}")]
        public async Task<IActionResult> GetComplain(int id)
        {
            // searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO=3";
            string searchString = $"WHERE ID_RECLAMACION > 0 AND ID_PERSONA_RECLAMACION = {id}";

            if (CAnswer.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CAnswer.Select(searchString));
        }
        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString = $"WHERE ID_RESPUESTA={id}";

            if (CAnswer.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CAnswer.Select(searchString));
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CAnswer obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(await obj.Update());
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           return Ok(await CAnswer.Delete(id));
        }
    }
}

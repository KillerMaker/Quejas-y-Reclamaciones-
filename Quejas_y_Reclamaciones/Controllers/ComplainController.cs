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
    public class ComplainController : ControllerBase
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> post(CComplain complain)
        {
            if (complain.id.HasValue)
                return BadRequest("Informacion Redundante (Id)");
            else
                return Ok(await complain.Insert());
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> put(CComplain complain)
        {
            if (!complain.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(await complain.Update());
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(await CComplain.Delete(id.Value));
        }

        [HttpGet("Mostrar")]
        public async Task<IActionResult> get(string searchString)
        {
            searchString += " WHERE ID_ESTADO !=3";

            if (CComplain.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CComplain.Select(searchString));
        }
        [HttpGet("Mostrar/{id?}")]
        public async Task<IActionResult> get(int id)
        {
          string searchString = $"WHERE ID_PERSONA= {id} WHERE ID_ESTADO !=3";
          return  Ok(await CComplain.Select(searchString));
        }
    }
}

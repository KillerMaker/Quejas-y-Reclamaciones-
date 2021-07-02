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
    public class ComplainController : ControllerBase,IController<CComplain>
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CComplain obj)
        {
            if (obj.id.HasValue)
                return BadRequest("Informacion Redundante (Id)");
            else
                return Ok(await obj.Insert());
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CComplain obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(await obj.Update());
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CComplain.Delete(id));
        }

        [HttpGet("Mostrar")]
        public async Task<IActionResult> Get(string searchString)
        {
            searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO=3";

            if (CComplain.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CComplain.Select(searchString));
        }
        [HttpGet("Mostrar/{id?}")]
        public async Task<IActionResult> Get(int id)
        {
          string searchString = $"WHERE ID_PERSONA= {id} AND ID_ESTADO !=3";
          return  Ok(await CComplain.Select(searchString));
        }
    }
}

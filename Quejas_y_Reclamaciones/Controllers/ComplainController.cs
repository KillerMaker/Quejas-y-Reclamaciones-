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
        public IActionResult post(CComplain complain)
        {
            if (complain.id.HasValue)
                return BadRequest("Informacion Redundante (Id)");
            else
                return Ok(complain.Insert().Result);
        }

        [HttpPut("Actualizar")]
        public IActionResult put(CComplain complain)
        {
            if (!complain.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(complain.Update().Result);
        }

        [HttpDelete("Eliminar")]
        public IActionResult delete(CComplain complain)
        {
            if (!complain.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(complain.Delete().Result);
        }

        [HttpGet("Mostrar")]
        public IActionResult get(string searchString)
        {
            if (CComplain.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(CComplain.Select(searchString).Result);
        }
        [HttpGet("Mostrar/{id}")]
        public IActionResult get(int id)
        {
            string searchString = $"WHERE ID_QUEJA= {id}";
          return  Ok(CComplain.Select(searchString).Result);
        }
    }
}

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
                return Ok(complain.Insert());
        }

        [HttpPut("Actualizar")]
        public IActionResult put(CComplain complain)
        {
            if (!complain.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(complain.Update());
        }

        [HttpDelete("Eliminar")]
        public IActionResult delete(CComplain complain)
        {
            if (!complain.id.HasValue)
                return BadRequest("Informacion Insuficiente de la queja (ID)");
            else
                return Ok(complain.Delete());
        }

        [HttpGet("Mostrar")]
        public IActionResult get(string searchString)
        {
            if (CComplain.Select(searchString).Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(CComplain.Select(searchString));
        }
    }
}

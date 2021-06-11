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
    public class ClaimController : ControllerBase
    {
        [HttpPost("Insertar")]
        public IActionResult Post(CClaim claim)
        {
            if (claim.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(claim.Insert().Result);
        }
        
        [HttpGet("Mostrar")]
        public IActionResult Get(string searchString)
        {
            if (CClaim.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(CClaim.Select(searchString).Result);
        }

        [HttpPut("Actualizar")]
        public IActionResult Put(CClaim claim)
        {
            if (!claim.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(claim.Update().Result);
        }

        [HttpDelete("Eliminar")]
        public IActionResult Delete(CClaim claim)
        {
            if (!claim.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(claim.Delete().Result);
        }
    }
}

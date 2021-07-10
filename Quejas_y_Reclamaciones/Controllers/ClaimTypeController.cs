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
    public class ClaimTypeController : ControllerBase,IController<CClaimType>
    {
        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CClaimType.Delete(id));
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> Get(string searchString)
        {
            List<CClaimType> list = new List<CClaimType>();

            list = await CClaimType.Select(searchString);
            if (list.Count.Equals(0))
                return BadRequest("Recurso no encontrado");
            else
                return Ok(list);
        }

        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString = $"WHERE ID_TIPO_RECLAMACION ={id}";

            List<CClaimType> list = new List<CClaimType>();

            list = await CClaimType.Select(searchString);
            if (list.Count.Equals(0))
                return BadRequest("Recurso no encontrado");
            else
                return Ok(list);
        }

        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CClaimType obj)
        {
            return Ok(await obj.Insert());
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CClaimType obj)
        {
            return Ok(await obj.Update());
        }
    }
}

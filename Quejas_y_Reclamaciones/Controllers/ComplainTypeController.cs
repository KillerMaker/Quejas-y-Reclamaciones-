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
    public class ComplainTypeController : ControllerBase,IController<CComplainType>
    {
        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CComplainType.Delete(id));
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult>Get(string searchString)
        {
            List<CComplainType> list = new List<CComplainType>();

            list=await CComplainType.Select(searchString);
            if (list.Count.Equals(0))
                return BadRequest("Recurso no encontrado");
            else
                return Ok(list);
        }
        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return CComplainType.Select($"WHERE ID_TIPO_QUEJA = {id}").Result.Count.Equals(0) ? BadRequest(): Ok(await CComplainType.Select($"WHERE ID_TIPO_QUEJA = {id}"));
        }
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CComplainType obj)
        {
            return Ok(await obj.Insert());
        }
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CComplainType obj)
        {
            return Ok(await obj.Update());
        }
    }
}

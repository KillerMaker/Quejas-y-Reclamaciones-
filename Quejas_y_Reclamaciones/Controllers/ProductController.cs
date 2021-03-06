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
    public class ProductController : ControllerBase,IController<CProduct>
    {   
        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CProduct.Delete(id));
        }
        
        [HttpGet("Mostrar")]
        public async Task<IActionResult> Get(string searchString)
        {
            if (CProduct.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CProduct.Select(searchString));
        }
        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString = $"WHERE ID_PRODUCTO ={id}";
            if (CProduct.Select(searchString).Result.Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(await CProduct.Select(searchString));
        }


        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CProduct obj)
        {
            if (obj.id.HasValue)
                return BadRequest("Informacion Redundante (ID)");
            else
                return Ok(await obj.Insert());
        }


        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CProduct obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion de busqueda insuficiente (ID)");
            else
                return Ok(await obj.Update());
        }
    }
}

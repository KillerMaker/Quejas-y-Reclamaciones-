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
    public class ProductController : ControllerBase
    {   
        [HttpDelete("Eliminar")]
        public IActionResult Delete(CProduct product)
        {
            if (!product.id.HasValue)
                return BadRequest("Informacion de busqueda insuficiente (ID)");
            else
                return Ok(product.Delete());
        }

        [HttpGet("Mostrar")]
        public IActionResult Get(string searchString)
        {
            if (CProduct.Select(searchString).Count.Equals(0))
                return NotFound("Recurso no encontrado");
            else
                return Ok(CPerson.Select(searchString));
        }


        [HttpPost("Insertar")]
        public IActionResult Post(CProduct product)
        {
            if (product.id.HasValue)
                return BadRequest("Informacion Redundante (ID)");
            else
                return Ok(product.Insert());
        }

        [HttpPut("Actualizar")]
        public IActionResult Put(CProduct product)
        {
            if (!product.id.HasValue)
                return BadRequest("Informacion de busqueda insuficiente (ID)");
            else
                return Ok(product.Update());
        }
    }
}

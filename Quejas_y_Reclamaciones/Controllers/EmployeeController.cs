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
    public class EmployeeController : ControllerBase
    {
        [HttpPost("Insertar")]
        public async Task<IActionResult> Post(CEmployee obj)
        {
            if (obj.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(await obj.Insert());
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CEmployee obj)
        {
            if (!obj.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(await obj.Update());
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> Get(string searchString)
        {
            searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO!=3";

            if (CEmployee.Select(searchString).Result.Count.Equals(0))
                return BadRequest("Recurso no Encontrado");
            else
                return Ok(await CEmployee.Select(searchString));
        }

        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString = $"WHERE ID_PERSONA ={id}";

            if (CEmployee.Select(searchString).Result.Count.Equals(0))
                return BadRequest("Recurso no Encontrado");
            else
                return Ok(await CEmployee.Select(searchString));
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(await CEmployee.Delete(id.Value));
        }

    }
}

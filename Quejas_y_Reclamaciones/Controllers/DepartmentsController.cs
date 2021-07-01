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
    public class DepartmentsController : ControllerBase
    {
        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult>get(string searchString)
        {
            searchString = (searchString != null) ? searchString += "AND ID_ESTADO!=3" : "WHERE ID_ESTADO!=3";
            return Ok(await CDepartment.Select(null));
        }
        [HttpPost("Insertar")]
        public async Task<IActionResult>post(CDepartment department)
        {
            return Ok(await department.Insert());
        }
        [HttpDelete("Eliminar/{id?:int}")]
        public async Task<IActionResult> delete(int id)
        {
            return Ok(await CDepartment.Delete(id));
        }
        [HttpPut("Actualizar")]
        public async Task<IActionResult> update(CDepartment department)
        {
            return Ok(await department.Update());
        }
    }
}

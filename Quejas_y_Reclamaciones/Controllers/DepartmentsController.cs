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
        public async Task<IActionResult>Get(string searchString)
        {
            searchString = (searchString != null) ? searchString += "AND D.ID_ESTADO!=3" : "WHERE D.ID_ESTADO!=3";
            return Ok(await CDepartment.Select(searchString));
        }

        [HttpGet("Mostrar/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            string searchString=$"WHERE D.ID_DEPARTAMENTO ={id}";
            return Ok(await CDepartment.Select(searchString));
        }

        [HttpPost("Insertar")]
        public async Task<IActionResult>Post(CDepartment obj)
        {
            return Ok(await obj.Insert());
        }

        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await CDepartment.Delete(id));
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CDepartment obj)
        {
            return Ok(await obj.Update());
        }
    }
}

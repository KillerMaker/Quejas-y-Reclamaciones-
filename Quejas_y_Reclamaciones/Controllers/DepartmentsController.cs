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
        [HttpGet("Mostrar")]
        public async Task<IActionResult>get()
        {
            return Ok(await CDepartment.Select(null));
        }
        [HttpPost("Insertar")]
        public async Task<IActionResult>post(CDepartment department)
        {
            return Ok(await department.Insert());
        }
    }
}

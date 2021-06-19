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
        [HttpGet("Mostrar/{id?:int}")]
        public async Task<IActionResult>get(int? id)
        {
            List<CDepartment> departments = new List<CDepartment>();

            departments = await CDepartment.Select("");

            if (departments.Count.Equals(0))
                return BadRequest("MMG");
            else
                return Ok(departments);
        }
    }
}

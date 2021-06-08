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
        public IActionResult Post(CEmployee employee)
        {
            if (employee.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(employee.Insert());
        }

    }
}

﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Post(CEmployee employee)
        {
            if (employee.id.HasValue)
                return BadRequest("Informacion redundante (ID)");
            else
                return Ok(await employee.Insert());
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Put(CEmployee employee)
        {
            if (!employee.id.HasValue)
                return BadRequest("Informacion Insuficiente (ID)");
            else
                return Ok(await employee.Update());
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> Get(string searchString)
        {
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

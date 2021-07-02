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
    public class ComplainTypeController : ControllerBase,IController<CComplainType>
    {
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
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

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(CComplainType obj)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put(CComplainType obj)
        {
            throw new NotImplementedException();
        }
    }
}

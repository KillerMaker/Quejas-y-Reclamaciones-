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
    public class ClaimTypeController : ControllerBase,IController<CClaimType>
    {
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Mostrar/{searchString?}")]
        public async Task<IActionResult> Get(string searchString)
        {
            List<CClaimType> list = new List<CClaimType>();

            list = await CClaimType.Select(searchString);
            if (list.Count.Equals(0))
                return BadRequest("Recurso no encontrado");
            else
                return Ok(list);
        }

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(CClaimType obj)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put(CClaimType obj)
        {
            throw new NotImplementedException();
        }
    }
}

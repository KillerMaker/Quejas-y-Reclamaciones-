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
    public class ComplainController : ControllerBase
    {
        [HttpPost("Insertar")]
        public object post(CComplain complain) => complain.Insert();

        [HttpPut("Actualizar")]
        public string put(CComplain complain) => complain.Update();

        [HttpDelete("Eliminar")]
        public string delete(CComplain complain) => complain.Delete();

        [HttpGet("Mostrar")]
        public List<CComplain> get(string searchString) => CComplain.Select(searchString);
    }
}

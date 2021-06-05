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
    public class PersonController : ControllerBase
    {
        [HttpPost("Insertar")]
        public object Post(CPerson person) => person.Insert();

        [HttpDelete("Eliminar")]
        public string delete(CPerson person) => person.Delete();

        [HttpPut("Actualizar")]
        public string put(CPerson person) => person.Update();

        [HttpPost("Mostrar")]
        public List<CPerson> get(string searchString) => CPerson.Select(searchString);

    }
}

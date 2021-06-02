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
    public class ProductController : ControllerBase
    { 
        [HttpDelete("Eliminar")]
        public string Delete(CProduct product)=> product.Delete();

        [HttpGet("Mostrar")]
        public List<CProduct> Get(string searchString) => CProduct.Select(searchString);

        [HttpPost("Insertar")]
        public string Post(CProduct product) => product.Insert();

        [HttpPut("Actualizar")]
        public string Put(CProduct product) => product.Update();
    }
}

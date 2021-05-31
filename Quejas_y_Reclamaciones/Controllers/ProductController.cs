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
        [HttpPost("Eliminar")]
        public string Delete(CProduct product)=> product.Delete();

        [HttpPost("Mostrar")]
        public List<CProduct> Select(string searchString) => CProduct.Select(searchString);

    }
}

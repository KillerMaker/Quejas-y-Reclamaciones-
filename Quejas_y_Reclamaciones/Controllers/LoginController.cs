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
     public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(CLogin login)
        {

            bool checkUserName =login.CheckUserName().Result;
            bool checkPassword =login.CheckPassword().Result;

            if (!checkUserName)
                return NotFound("Usuario Invalido");

            else if (!checkPassword)
                return NotFound("Clave Incorrecta");

            else if (checkUserName && checkPassword)
                return Ok(login.loginIntoApplication().Result);

            return NotFound();

        }
    }
}

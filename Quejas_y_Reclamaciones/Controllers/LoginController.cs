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
        public async Task<IActionResult> Post(CLogin login)
        {

            bool checkUserName =await login.CheckUserName();
            bool checkPassword = await login.CheckPassword();

            if (!checkUserName)
                return NotFound("Usuario Invalido");

            else if (!checkPassword)
                return NotFound("Clave Incorrecta");

            else if (checkUserName && checkPassword)
                return Ok(await login.loginIntoApplication());

            return NotFound();

        }
    }
}

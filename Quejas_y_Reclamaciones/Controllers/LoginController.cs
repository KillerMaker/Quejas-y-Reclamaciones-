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
        public async Task<IActionResult> Post(CUser user)
        {

            bool checkUserName =await user.CheckUserName();
            bool checkPassword = await user.CheckPassword();

            if (!checkUserName)
                return NotFound("Usuario Invalido");

            else if (!checkPassword)
                return NotFound("Clave Incorrecta");

            else if (checkUserName && checkPassword)
                return Ok(await user.loginIntoApplication());

            return NotFound();

        }
    }
}

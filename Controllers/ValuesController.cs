using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Peoplee.API.BusinessLayer;
using Peoplee.API.Models;

namespace Peoplee.API.Controllers
{
    [Authorize]
     [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

            UserManager userManager;
            public ValuesController(UserManager manager)
            {
                userManager = manager;
            }

            [HttpGet("list")]
            public ActionResult List(){

                List<User> userList = userManager.List();

               //  Console.WriteLine();

             return Ok(userList);
         }
    }
}
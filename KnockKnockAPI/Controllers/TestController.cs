using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KnockKnockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        [Route("test")]
        public IActionResult Get([BindRequired, FromQuery] int n)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("its a bad request");
            }
            return Ok("Hello");
        }
    }
}
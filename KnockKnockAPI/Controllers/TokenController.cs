using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnockKnockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        // GET: api/Token
        [HttpGet]
        public Guid Get()
        {
            return Guid.Parse("cd41b7ca-4ce1-45de-8eae-8d1f314cd4a7");
        }
    }
}

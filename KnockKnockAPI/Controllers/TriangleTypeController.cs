using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KnockKnockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleTypeController : ControllerBase
    {

        private readonly ILogger _logger;
        public TriangleTypeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FibonacciController>();
        }


        [HttpGet]
        public ActionResult<string> Get(int a, int b, int c)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "The request is invalid."});
            }

            _logger.LogInformation("WhatShapeIsThis  a:{0}, b:{1}, c:{2}", a, b, c);

            if (a == b && b == c && a > 0 && b > 0 && c > 0)
            {
                 _logger.LogInformation(string.Format("WhatShapeIsThis  result {0} ", TriangleType.Equilateral));
                return TriangleType.Equilateral.ToString();
            }

            if (a >= b + c || b >= a + c || c >= b + a || a < 0 || b < 0 || c < 0)
            {
                 _logger.LogInformation(string.Format("WhatShapeIsThis  result {0} ", TriangleType.Error));
                return TriangleType.Error.ToString();
            }

            if (a == b || b == c || c == a)
            {
                 _logger.LogInformation(string.Format("WhatShapeIsThis  result {0} ", TriangleType.Isosceles));
                return TriangleType.Isosceles.ToString();
            }

             _logger.LogInformation(string.Format("WhatShapeIsThis  result {0} ", TriangleType.Scalene));

            Trace.Flush();
            return TriangleType.Scalene.ToString();
        }

        public enum TriangleType
        {
            Error = 0,

            Equilateral = 1,

            Isosceles = 2,

            Scalene = 3
        }
    }
}
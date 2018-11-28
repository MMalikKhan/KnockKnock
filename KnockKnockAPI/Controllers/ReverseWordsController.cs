using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KnockKnockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseWordsController : ControllerBase
    {
        private readonly ILogger<ReverseWordsController> _logger;
        public ReverseWordsController(ILogger<ReverseWordsController> logger)
        {
            _logger = logger;//.CreateLogger<FibonacciController>();
            _logger.LogTrace("Hello");
        }

        // GET: api/Token
        [HttpGet]
        public ActionResult<string> Get(string s)
        {
             _logger.LogInformation(string.Format("ReverseWords  input '{0}'", s));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            var result = string.Empty;

            //split the word at space
            var splitArray = s.Split(' ');

            for (var i = 0; i < splitArray.Length; i++)
            {
                var chars = splitArray[i].ToCharArray();
                Array.Reverse(chars);
                var reverseWord = new string(chars);
                if (i == 0)
                {
                    result = result + reverseWord;
                }
                else if (reverseWord == "")
                {
                    result = result + " ";
                }
                else
                {
                    result = string.Format("{0} {1}", result, reverseWord);
                }
            }

             _logger.LogInformation(string.Format("ReverseWords  Result '{0}' ", result));
            Trace.Flush();
            return result;
        }
    }
}
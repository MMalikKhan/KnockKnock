using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KnockKnockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {

        private readonly ILogger _logger;
        public FibonacciController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FibonacciController>();
        }
        /// <summary>
        /// method to get fibonacci number based on the provided value
        /// </summary>
        /// <param name="n">Its a number of type long</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Int64> Get(long n)
        {
            long result;
            _logger.LogInformation("FibonacciNumber " + n);
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var f = new FibonacciHelper();
                result = f.Calculate(n);

                _logger.LogInformation("FibonacciNumber result " + result);
                
            }
            catch (Exception ex)
            {
                return BadRequest("Error");
            }

            return result; 
        }

        public class FibonacciHelper
        {
            private readonly Dictionary<long, long> _dictionary;

            public FibonacciHelper()
            {
                _dictionary = new Dictionary<long, long>();
            }

            public long Calculate(long fibonacciNumber)
            {
                var isNagativeNumber = false;
                if (fibonacciNumber <= 0)
                {
                    fibonacciNumber = fibonacciNumber * -1;
                    isNagativeNumber = true;
                }

                // Fib(>92) will cause a 64-bit integer overflow.
                if (fibonacciNumber > 92 || fibonacciNumber < -92)
                {
                    throw new ArgumentOutOfRangeException("fibonacciNumber", "Fib(>92) will cause a 64-bit integer overflow.");
                }

                long returnValue;
                long previous1;
                long previous2;

                //first try to get it from the dictionary
                if (_dictionary.TryGetValue(fibonacciNumber, out returnValue))
                {
                    // if the ordinalPosition is negative and even then resultValue will be negative
                    return isNagativeNumber && fibonacciNumber % 2 == 0 ? returnValue * -1 : returnValue;
                }

                //Handle special cases of n == 0,1, or 2 (Priming the function).
                if (fibonacciNumber == 0)
                {
                    _dictionary.Add(fibonacciNumber, 0);
                    return 0;
                }

                if (fibonacciNumber == 1 || fibonacciNumber == 2 || fibonacciNumber == -1 || fibonacciNumber == -2)
                {
                    _dictionary.Add(fibonacciNumber, 1);
                    returnValue = 1;
                    // if the ordinalPosition is negative and even then resultValue will be negative
                    return isNagativeNumber && fibonacciNumber % 2 == 0 ? returnValue * -1 : returnValue;
                }

                //If we already have the previous ordinalPosition, use that value to calculate the next.
                if (_dictionary.TryGetValue(fibonacciNumber - 1, out previous1))
                {
                    _dictionary.TryGetValue(fibonacciNumber - 2, out previous2);
                    //It's safe to assume if we found n-1, n-2 is there.
                    returnValue = previous1 + previous2;
                    _dictionary.Add(fibonacciNumber, returnValue);
                    // if the ordinalPosition is negative and even then resultValue will be negative
                    return isNagativeNumber && fibonacciNumber % 2 == 0 ? returnValue * -1 : returnValue;
                }

                //If we've gotten here, there's a gap between the last ordinalPosition and the one requested.
                if (_dictionary.Count > 2)
                {
                    //start at the next missing fibonacci number
                    for (var i = _dictionary.Count; i <= fibonacciNumber; i++)
                    {
                        _dictionary.TryGetValue(_dictionary.Count - 1, out previous1);
                        _dictionary.TryGetValue(_dictionary.Count - 2, out previous2);
                        returnValue = previous1 + previous2;
                        _dictionary.Add(i, returnValue);
                    }
                    // if the ordinalPosition is negative and even then resultValue will be negative
                    return isNagativeNumber && fibonacciNumber % 2 == 0 ? returnValue * -1 : returnValue;
                }

                //If all else fails, start at the beginning.
                var fib = new FibonacciHelper();
                for (var i = 0; i <= fibonacciNumber; i++)
                {
                    _dictionary.Add(i, fib.Calculate(i));
                }
                _dictionary.TryGetValue(fibonacciNumber, out returnValue);
                // if the ordinalPosition is negative and even then resultValue will be negative
                return isNagativeNumber && fibonacciNumber % 2 == 0 ? returnValue * -1 : returnValue;
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using RestCalculator.Exceptions;
using System.Net;

namespace RestCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(double firstNumber, double secondNumber)
        {
            return Ok((firstNumber + secondNumber).ToString());
        }

        [HttpGet("subtract/{firstNumber}/{secondNumber}")]
        public IActionResult Subtract(double firstNumber, double secondNumber)
        {
            return Ok((firstNumber - secondNumber).ToString());
        }

        [HttpGet("multiply/{firstNumber}/{secondNumber}")]
        public IActionResult Multiply(double firstNumber, double secondNumber)
        {
            return Ok((firstNumber - secondNumber).ToString());
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(double firstNumber, double secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new InvalidRequestException("You can not divide by 0.", "Error", "Calculator", nameof(Division));
            }

            return Ok((firstNumber - secondNumber).ToString());
        }

        [HttpGet("sqr/{firstNumber}")]
        public IActionResult Sqrt(double firstNumber)
        {
            return Ok(Math.Sqrt(firstNumber).ToString());
        }
    }
}
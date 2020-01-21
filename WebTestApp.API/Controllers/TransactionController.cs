using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTestApp.API.Validations.Attributes;
using WebTestApp.Services.FileProcessors.Models;
using WebTestApp.Services.Transactions;
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
       
        public IActionResult UploadFile([FromForm] FileUploadModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _service.SaveFile(file.file);
            return Ok();
        }

        [HttpGet("byDateRange")]
        [ProducesResponseType(typeof(IList<TransactionModel>),200)]
        public IActionResult GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(_service.GetByDateRange(from, to));
        }

        [HttpGet("byStatus")]
        [ProducesResponseType(typeof(IList<TransactionModel>), 200)]
        public IActionResult GetByDateRange([FromQuery] string status)
        {
            return Ok(_service.GetByStatus(status));
        }

        [HttpGet("byCurrency")]
        [ProducesResponseType(typeof(IList<TransactionModel>), 200)]
        public IActionResult GetByCurrency([FromQuery] string code)
        {
            return Ok(_service.GetByCurrency(code));
        }

    }
}
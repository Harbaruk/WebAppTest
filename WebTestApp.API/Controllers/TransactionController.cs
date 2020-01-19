using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTestApp.API.Validations.Attributes;
using WebTestApp.Services.Transactions;

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
        [MaxFileSize(1 * 1024 * 1024)] // 1 Mb
        [SupportedFormats(".csv", ".xml")]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
            _service.SaveFile(file);
            return Ok();
        }

    }
}
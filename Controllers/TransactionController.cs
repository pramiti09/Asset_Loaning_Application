using asset_loaning_api.data;
using asset_loaning_api.models.domains;
using asset_loaning_api.models.DTOs;
using asset_loaning_api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace asset_loaning_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionservice;

        public TransactionController(ITransactionService transactionservice)
        {
            this.transactionservice = transactionservice;
            return;
        }
        
       
        //Creating a transaction

        [HttpPost]
        public IActionResult Create_transaction([FromBody] transactionDTO transactiondto)
        {
           var data = transactionservice.Create_transaction(transactiondto);
            return Ok(data);
        }

        // Update a transaction
        [HttpPut]
        public IActionResult UpdateTransaction([FromBody] UpdateTransactionDTO updatetransactiondto)
        {

            var data = transactionservice.UpdateTransaction(updatetransactiondto);
            return Ok(data);
        }

       
        //Get transaction by filter
        [HttpGet]
        public IActionResult GetTransaction(Guid userid, [FromQuery] Guid? transactionId, [FromQuery] Guid? supervisorId,[FromQuery] Guid? studentId, [FromQuery] Guid? assetId, [FromQuery] string? transaction_date) 
        {
            var data = transactionservice.GetTransaction(userid, transactionId,supervisorId,studentId, assetId, transaction_date );
            return Ok(data);
        }
    }

}
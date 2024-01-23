using asset_loaning_api.models.DTOs;
using asset_loaning_api.models.domains;
using Microsoft.AspNetCore.Mvc;

namespace asset_loaning_api.Services
{
    public interface ITransactionService
    {
        public transactionDTO Create_transaction(transactionDTO transactiondto);

        public transactions UpdateTransaction([FromBody] UpdateTransactionDTO updatetransactiondto);
        public List<GetTransaction>  GetTransaction(Guid userid, Guid? transactionId,Guid? supervisorId,Guid? studentId, Guid? assetId, string? transaction_date);

    }
}

using asset_loaning_api.models.domains;
using asset_loaning_api.models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace asset_loaning_api.Repositories
{
    public interface ITransactionRepository
    {
        public transactionDTO Create_transaction([FromBody] transactionDTO transactiondto);
        public transactions UpdateTransaction([FromBody] UpdateTransactionDTO updatetransactiondto);
        public List<GetTransaction>  GetTransaction(Guid userid, Guid? transactionId,Guid? supervisorId, Guid? studentId ,Guid? assetId, string? transaction_date);
    }
}

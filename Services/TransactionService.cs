using asset_loaning_api.models.DTOs;
using asset_loaning_api.data;
using asset_loaning_api.models.domains;
using Microsoft.AspNetCore.Mvc;
using asset_loaning_api.Repositories;

namespace asset_loaning_api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository repository;

        public TransactionService(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        public transactionDTO Create_transaction(transactionDTO transactiondto)
        {
            return repository.Create_transaction(transactiondto);
        }

        public List<GetTransaction> GetTransaction(Guid userid, Guid? transactionId,Guid? supervisorId, Guid? studentId, Guid? assetId, string? transaction_date)
        {
            return repository.GetTransaction(userid, transactionId,supervisorId,studentId, assetId, transaction_date);
        }

        public transactions UpdateTransaction([FromBody] UpdateTransactionDTO updatetransactiondto)
        {
            return repository.UpdateTransaction(updatetransactiondto);
        }
    }
}

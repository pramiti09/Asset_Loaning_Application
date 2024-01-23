using asset_loaning_api.data;
using asset_loaning_api.models.domains;
using asset_loaning_api.models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;

namespace asset_loaning_api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly asset_loaningdbcontext dbContext;
        public TransactionRepository(asset_loaningdbcontext dbContext)
        {
            this.dbContext = dbContext;
            return;
        }

        public transactionDTO Create_transaction([FromBody] transactionDTO transactiondto)
        {
            //Only supervisor can create transaction
            var user = dbContext.UserDetails.FirstOrDefault(u => u.userId == transactiondto.requesting_userid && u.role == "Supervisor");
            if (user == null)
            {
                throw new KeyNotFoundException("Only supervisor can create transactions");
            }
            // Validate loan transaction request data
            if (transactiondto.transaction_type == "loan")
            {

                var supervisor = dbContext.UserDetails.FirstOrDefault(u => u.userId == transactiondto.supervisorId && u.role == "Supervisor");
                if (supervisor == null)
                {
                   
                     throw new KeyNotFoundException("Invalid supervisor details"); ;
                }

                var student = dbContext.UserDetails.FirstOrDefault(u => u.userId == transactiondto.studentID && u.role == "Student");
                if (student == null)
                {
                    throw new KeyNotFoundException("Invalid student details");
                }

                var asset = dbContext.AssetDetails.FirstOrDefault(a => a.assetid == transactiondto.assetId && a.occupied == 0);
                if (asset == null)
                {
                    throw new KeyNotFoundException("Invalid asset details");
                }
                var date = DateOnly.MaxValue;
                try
                {
                    date = DateOnly.Parse(transactiondto.transaction_date);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                var new_transaction = new transactions
                {
                    transactionID = Guid.NewGuid(),
                    supervisorId = supervisor.userId,
                    studentID = student.userId,
                    assetId = asset.assetid,
                    transaction_type = "loan",
                    transaction_date = date,

                };

                dbContext.transactions.Add(new_transaction);
                asset.occupied = 1;
            }
            else
            {
                var returning_supervisor = dbContext.UserDetails.FirstOrDefault(u => u.userId == transactiondto.returning_supervisorId && u.role == "Supervisor");
                if (returning_supervisor == null)
                {
                    throw new KeyNotFoundException("Invalid Returning Supervisor Details");
                }
                var previous_details = dbContext.transactions.FirstOrDefault(p => p.transactionID == transactiondto.loan_transactionID);
                if (previous_details == null)
                {
                    throw new KeyNotFoundException("Transaction Not Found"); ;
                }
                var asset = dbContext.AssetDetails.FirstOrDefault(a => a.assetid == previous_details.assetId && a.occupied == 1);
                if (asset == null)
                {
                    throw new KeyNotFoundException("Invalid asset details or asset not found");
                }
                var date = DateOnly.MaxValue;
                try
                {
                    date = DateOnly.Parse(transactiondto.transaction_date);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                var new_transaction = new transactions
                {
                    transactionID = Guid.NewGuid(),
                    supervisorId = previous_details.supervisorId,
                    studentID = previous_details.studentID,
                    assetId = previous_details.assetId,
                    transaction_type = "return",
                    transaction_date = date,
                    returning_supervisorId = transactiondto.returning_supervisorId
                };
                dbContext.transactions.Add(new_transaction);
                asset.occupied = 0;

            }

            dbContext.SaveChanges();
            return transactiondto;
        }


        // Fetch transactions
        public List<GetTransaction> GetTransaction(Guid userid, Guid? transactionId,Guid? supervisorId, Guid? studentId, Guid? assetId, string? transaction_date)
        {
            List<GetTransaction> transaction = new List<GetTransaction>();
            var TransactionRecords = new List<transactions>();
            
            //Checking for requesting user
            var user = dbContext.UserDetails.FirstOrDefault(u => u.userId == userid);
            if (user == null)
            {
                throw new KeyNotFoundException("invalid user details");
            }
            // if requesting user is supervisor then return all their transactions
            //if (user.role == "Supervisor" && transactionId == null && studentId == null && assetId == null && loan_date == null && return_date == null) 
            //{
            /*TransactionRecords = dbContext.transactions.Where(t => t.supervisorId == userid).ToList();
            foreach (var i in TransactionRecords) 
            {
                transaction.Add(i);
            }*/
            //}
            if (transactionId == null && supervisorId == null && studentId == null && assetId == null && transaction_date == null )
            {
                throw new KeyNotFoundException("Choose filters");
            }
            if (user.role == "Student")
            {
                if (studentId != null && studentId!= userid)
                {
                    throw new KeyNotFoundException("Cannot access other students records");
                }
                else if (userid == studentId )
                {
                    TransactionRecords = dbContext.transactions.Where(s => s.studentID == userid).ToList();
                }
                
            }
            else
            {
                TransactionRecords = dbContext.transactions.ToList();
            }
            //TransactionRecords = dbContext.transactions.ToList();
            //var GetTransaction = new GetTransaction();
            foreach (var i in TransactionRecords) 
            {
                var GetTransaction = new GetTransaction();
                var supervisor = dbContext.UserDetails.Where(u => u.userId == i.supervisorId).FirstOrDefault();
                var return_supervisor = dbContext.UserDetails.Where(u => u.userId == i.returning_supervisorId).FirstOrDefault();
                var student = dbContext.UserDetails.Where (u => u.userId == i.studentID).FirstOrDefault();
                var asset = dbContext.AssetDetails.Where(u => u.assetid == i.assetId).FirstOrDefault();
                GetTransaction.transactionId = i.transactionID;
                GetTransaction.studentId = i.studentID;
                if (student != null)
                {
                    GetTransaction.StudentName = $"{i.student.firstName} {i.student.lastName}";
                }
                GetTransaction.supervisorId = i.supervisorId;
                if (supervisor != null)
                {
                    GetTransaction.Loaning_Supervisor = $"{i.supervisor.firstName} {i.supervisor.lastName}";
                }
                GetTransaction.assetId = i.assetId;
                if (asset != null)
                {
                    GetTransaction.Asset = i.asset.name;
                }
                
                GetTransaction.transaction_date = i.transaction_date;
                GetTransaction.transaction_type = i.transaction_type;
                GetTransaction.returning_supervisorId = i.returning_supervisorId;
                if (return_supervisor != null)
                { GetTransaction.Returning_Supervisor = $"{return_supervisor.firstName} {return_supervisor.lastName}"; }

                transaction.Add(GetTransaction);
            }
            
            List<GetTransaction> FilteredTransactions = new List<GetTransaction>();
            if (supervisorId != null && user.role == "Supervisor")
            {
                foreach (var i in transaction)
                {
                    if (i.supervisorId == supervisorId)
                    {
                        FilteredTransactions.Add(i);
                    }
                }
                transaction.Clear();
                transaction.AddRange(FilteredTransactions);
                FilteredTransactions.Clear();
            }
            else if(supervisorId != null && user.role == "Student")
            {
                throw new KeyNotFoundException("Students cannot access supervisor's records");
            }
            if (studentId != null && user.role == "Supervisor")
            {

                foreach (var i in transaction)
                {
                    if (i.studentId == studentId)
                    {
                        FilteredTransactions.Add(i);
                    }
                }
                transaction.Clear();
                transaction.AddRange(FilteredTransactions);
                FilteredTransactions.Clear();
            }   

            if (user.role == "Supervisor" && transactionId != null)
            {
                var transactiondetails = dbContext.transactions.Where(t => t.transactionID == transactionId);
                if (transactiondetails == null)
                {
                    throw new KeyNotFoundException("No such transaction exists or invalid transaction ID");
                }
                else
                {
                    foreach (var i in transaction)
                    {
                        if (i.transactionId == transactionId)
                        {
                            FilteredTransactions.Add(i);
                        }

                    }
                    transaction.Clear();
                    transaction.AddRange(FilteredTransactions);
                    FilteredTransactions.Clear();
                }
            }else if(user.role == "Student" && transactionId != null)
            {
                throw new KeyNotFoundException("Student cannot access through transaction ID");
            }

            if (assetId != null)
            {
                foreach (var i in transaction)
                {
                    if (i.assetId == assetId)
                    {
                        FilteredTransactions.Add(i);
                    }

                }
                transaction.Clear();
                transaction.AddRange(FilteredTransactions);
                FilteredTransactions.Clear();
            }
            if (!string.IsNullOrEmpty(transaction_date) && (user.role == "Student" || user.role == "Supervisor"))
            {
                var date = DateOnly.MaxValue;
                try
                {
                    date = DateOnly.Parse(transaction_date);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                foreach (var i in transaction)
                {
                    if (i.transaction_date == date)
                    {
                        FilteredTransactions.Add(i);
                    }

                }
                transaction.Clear();
                transaction.AddRange(FilteredTransactions);
                FilteredTransactions.Clear();
            }
            

            return transaction;
        }
        // Updating a transaction
        public transactions UpdateTransaction([FromBody] UpdateTransactionDTO updatetransactiondto)
        {
            var user = dbContext.UserDetails.FirstOrDefault(u => u.userId == updatetransactiondto.requesting_userid && u.role == "Supervisor");
            if (user == null)
            {
                throw new KeyNotFoundException("Only supervisors can update transaction");
            }

            var existing_transaction = dbContext.transactions.FirstOrDefault(e => e.transactionID == updatetransactiondto.transactionID);
            if (existing_transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found  invalid student details");
            }
            /*
            string Supervisorid = updatetransactiondto.supervisorId.ToString();
            string Studentid = updatetransactiondto.studentID.ToString();
            string Assetid = updatetransactiondto.assetId.ToString();
            string ReturningSupervisorid = updatetransactiondto.returning_supervisorId.ToString();
            */

            if (updatetransactiondto.supervisorId != null)
            {
                var supervisor = dbContext.UserDetails.FirstOrDefault(u => u.userId == updatetransactiondto.supervisorId && u.role == "Supervisor");
                if (supervisor == null)
                {
                    throw new KeyNotFoundException("Invalid supervisor details");
                }
                else
                {
                    existing_transaction.supervisorId = supervisor.userId;
                }

            }
            if (updatetransactiondto.studentID != null)
            {

                var student = dbContext.UserDetails.FirstOrDefault(u => u.userId == updatetransactiondto.studentID && u.role == "Student");
                if (student == null)
                {
                    throw new KeyNotFoundException("Invalid student details");
                }
                else
                {
                    existing_transaction.studentID = student.userId;
                }
            }


            if (updatetransactiondto.assetId != null)
            {
                var asset = dbContext.AssetDetails.FirstOrDefault(a => a.assetid == updatetransactiondto.assetId && a.occupied == 0);
                if (asset == null)
                {
                    throw new KeyNotFoundException("Invalid asset details or asset not available");
                }
                else
                {
                    existing_transaction.assetId = asset.assetid;
                }
            }

            if (updatetransactiondto.returning_supervisorId != null)
            {

                var returning_supervisor = dbContext.UserDetails.FirstOrDefault(u => u.userId == updatetransactiondto.returning_supervisorId && u.role == "Supervisor");
                if (returning_supervisor == null)
                {
                    throw new KeyNotFoundException("Invalid returning supervisor details");
                }
                else
                {
                    existing_transaction.returning_supervisorId = returning_supervisor.userId;
                }
            }

            if (updatetransactiondto.transaction_type != null)
            {
                if (updatetransactiondto.transaction_type == "loan" || updatetransactiondto.transaction_type == "return")
                {

                    existing_transaction.transaction_type = updatetransactiondto.transaction_type;
                }
                else
                {
                    throw new KeyNotFoundException("Invalid value for transaction type");
                }
            }
            if (updatetransactiondto.transaction_date != null)
            {
                var date = DateOnly.MaxValue;
                try
                {
                    date = DateOnly.Parse(updatetransactiondto.transaction_date);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException(ex.Message);
                }
                existing_transaction.transaction_date = date;
            }

            

            dbContext.SaveChanges();
            return existing_transaction;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.Services.Transactions
{
    public interface ITransactionService
    {

        bool SaveFile(IFormFile file);
        IList<TransactionModel> GetByCurrency(string currency);
        IList<TransactionModel> GetByDateRange(DateTime from, DateTime to);
        IList<TransactionModel> GetByStatus(string status);
    }
}

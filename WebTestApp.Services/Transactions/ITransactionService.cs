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
        IList<TransactionViewModel> GetByCurrency(string currency);
        IList<TransactionViewModel> GetByDateRange(DateTime from, DateTime to);
        IList<TransactionViewModel> GetByStatus(string status);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApp.Services.Transactions.Models
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApp.DAL.Entities
{
    public class TransactionEntity
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}

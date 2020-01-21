using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestApp.Services.Transactions.Models
{
    public class TransactionViewModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
    }
}

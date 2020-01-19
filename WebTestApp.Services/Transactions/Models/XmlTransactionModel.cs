using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WebTestApp.Services.Transactions.Models
{
    [XmlRoot("Transaction")]
    public class XmlTransactionModel
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement]
        public DateTime TransactionDate { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement]
        public string Status { get; set; }

    }

    public class PaymentDetails
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}

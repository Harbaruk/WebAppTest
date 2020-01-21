using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WebTestApp.Services.FileReaders.Models;
using WebTestApp.Services.Transactions.FileReaders;
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.Services.FileReaders
{
    public class XmlFileProcessor<T> : IFileProcessor<T> where T : class
    {
        public FileProcessingResult<T> ProcessFile(Stream fileStream)
        {
            var serializer = new XmlSerializer(typeof(XmlTransactionModel), new Type[] { typeof(PaymentDetails) });
            
            if (serializer.CanDeserialize(XmlReader.Create(fileStream)))
            {
                return new FileProcessingResult<T>
                {
                    ValidItems = (T[])(serializer.Deserialize(fileStream)),
                    InvalidItems = null,
                    IsValid = true
                };
            }
            using(var reader = XmlReader.Create(fileStream))
            {
                return null;
            }
        }
    }
}

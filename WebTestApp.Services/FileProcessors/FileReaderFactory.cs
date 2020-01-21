using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using WebTestApp.Services.Transactions.FileReaders;

namespace WebTestApp.Services.FileReaders
{

    public enum FileReaderTypes
    {
        CSV, 
        XML
    }

    public class FileProcessorFactory : IFileProcessorFactory
    {
        private readonly ILogger _logger;

        public FileProcessorFactory(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("Factory");
        }

        public IFileProcessor<T> GetFileReader<T>(string type) where T : class
        {
            switch(type)
            {
                case ".csv":
                    return new CsvFileProcessor<T>(_logger);
                case ".xml":
                    return new XmlFileProcessor<T>();
                default:
                    return null;
                
            }
        }
    }
}

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

    public class FileReaderFactory : IFileProcessorFactory
    {
        private readonly ILogger _logger;

        public FileReaderFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IFileProcessor<T> GetFileReader<T>(FileReaderTypes type) where T : class
        {
            switch(type)
            {
                case FileReaderTypes.CSV:
                    return new CsvFileProcessor<T>(_logger);

                default:
                    return null;
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using WebTestApp.Services.FileReaders.Models;
using WebTestApp.Services.Transactions.FileReaders;

namespace WebTestApp.Services.FileReaders
{
    public class CsvFileProcessor<T> : IFileProcessor<T> where T : class
    {
        private readonly ILogger _logger;

        public CsvFileProcessor(ILogger logger)
        {
            _logger = logger;
        }
        
        public FileProcessingResult<T> ProcessFile(Stream fileStream)
        {
            var result = new FileProcessingResult<T>
            {
                ValidItems = new List<T>(),
                InvalidItems = new List<object>(),
                IsValid = true
            };

            using (TextReader streamReader = new StreamReader(fileStream))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    int counter = 1;
                    while (csvReader.Read())
                    {
                        var record = csvReader.GetRecord<T>();

                        if(record != null)
                        {
                            result.ValidItems.Add(record);
                        }
                        else
                        {
                            var row = csvReader.Context.RawRecord;

                            _logger.LogError($"Invalid row #{counter}, details : {row}");

                            result.IsValid = false;
                            result.InvalidItems.Add(row);
                        }
                        counter++;
                    }
                }
            }

            return result;
        }
    }
}

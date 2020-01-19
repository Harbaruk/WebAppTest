using System;
using System.Collections.Generic;
using System.Text;
using WebTestApp.Services.Transactions.FileReaders;

namespace WebTestApp.Services.FileReaders
{
    public interface IFileProcessorFactory
    {
        IFileProcessor<T> GetFileReader<T>(FileReaderTypes type) where T : class;
    }
}

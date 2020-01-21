using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebTestApp.DAL.Entities;
using WebTestApp.Services.FileReaders.Models;

namespace WebTestApp.Services.Transactions.FileReaders
{
    public interface IFileProcessor<T> where T: class
    {
        FileProcessingResult<T> ProcessFile(Stream fileStream);
    }
}

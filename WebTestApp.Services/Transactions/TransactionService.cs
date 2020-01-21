using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebTestApp.DAL.Entities;
using WebTestApp.DAL.Infrastructure;
using WebTestApp.Services.FileReaders;
using WebTestApp.Services.Transactions.FileReaders;
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.Services.Transactions
{
    public class TransactionService : ITransactionService
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IFileProcessorFactory _factory;
        private readonly List<string> _supportedFileExtensions = new List<string> { ".csv", ".xml" };

        public TransactionService(AppDbContext dbContext, IMapper mapper, ILoggerFactory logger, IFileProcessorFactory factory)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger.CreateLogger<TransactionService>();
            _factory = factory;
        }

        public IList<TransactionViewModel> GetByCurrency(string currency)
        {
            var all = _dbContext.Set<TransactionEntity>().ToList();

            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.CurrencyCode == currency)
                .Select(x => _mapper.Map<TransactionViewModel>(x))
                .ToList();
        }

        public IList<TransactionViewModel> GetByDateRange(DateTime from, DateTime to)
        {
            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.TransactionDate >= from && x.TransactionDate <= to)
                .Select(x => _mapper.Map<TransactionViewModel>(x))
                .ToList();
        }

        public IList<TransactionViewModel> GetByStatus(string status)
        {
            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.Status == status)
                .Select(x => _mapper.Map<TransactionViewModel>(x))
                .ToList();
        }

        public bool SaveFile(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            var processor = _factory.GetFileReader<TransactionModel>(extension);


            using (var inputStream = file.OpenReadStream())
            {
                var result = processor.ProcessFile(inputStream);
                if(!result.IsValid)
                {
                    return false;
                }
                _dbContext.AddRange(result.ValidItems.Select(x => _mapper.Map<TransactionEntity>(x)));
            }

            _dbContext.SaveChanges();
            return true;
            
        }
        
    }
}

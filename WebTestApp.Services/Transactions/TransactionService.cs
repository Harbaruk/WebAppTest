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
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.Services.Transactions
{
    public class TransactionService : ITransactionService
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly List<string> _supportedFileExtensions = new List<string> { ".csv", ".xml" };

        public TransactionService(AppDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IList<TransactionModel> GetByCurrency(string currency)
        {
            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.CurrencyCode == currency)
                .Select(x => _mapper.Map<TransactionModel>(x))
                .ToList();
        }

        public IList<TransactionModel> GetByDateRange(DateTime from, DateTime to)
        {
            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.TransactionDate >= from && x.TransactionDate <= to)
                .Select(x => _mapper.Map<TransactionModel>(x))
                .ToList();
        }

        public IList<TransactionModel> GetByStatus(string status)
        {
            return _dbContext.Set<TransactionEntity>()
                .Where(x => x.Status == status)
                .Select(x => _mapper.Map<TransactionModel>(x))
                .ToList();
        }

        public bool SaveFile(IFormFile file)
        {
            return false;
        }
        
    }
}

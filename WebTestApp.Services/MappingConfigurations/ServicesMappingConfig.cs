using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WebTestApp.DAL.Entities;
using WebTestApp.Services.Transactions.Models;

namespace WebTestApp.Services.MappingConfigurations
{
    public class ServicesMappingConfig : Profile
    {
        public ServicesMappingConfig()
        {
            CreateMap<TransactionEntity, TransactionModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Payment, opt => opt.MapFrom(y => $"{y.Amount} {y.CurrencyCode}"))
                .ForMember(x => x.Status, opt => opt.ConvertUsing(new TransactionStatusConverter()));
        }
    }


    public class TransactionStatusConverter : IValueConverter<string, string>
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Approved", "A" },
            { "Failed", "R" },
            { "Rejected", "R" },
            { "Finished", "D" },
            { "Done", "R" },
        };

        public string Convert(string sourceMember, ResolutionContext context)
        {
            return _dictionary.ContainsKey(sourceMember) ? _dictionary[sourceMember] : null;
        }
    }
}

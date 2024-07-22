using AutoMapper;
using OfficeMart.Domain.Models.AppDbContext;
using System;

namespace OfficeMart.Business.Models
{
    public static class TransactionConfig
    {
        public static OfficeMartContext AppDbContext
        {
            get
            {
                return new OfficeMartContext();
            }
        }
    }
}

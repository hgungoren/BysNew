using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthlyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AccountID { get; set; }
        public string AccountGroup { get; set; }
        public string Account { get; set; }
        public string Department { get; set; }
        public string ExpenseType { get; set; }
        public string Product { get; set; }
        public string Proje { get; set; }
        public string Comment { get; set; }
        public string Month { get; set; }
        public double Year { get; set; }
        public int Unit { get; set; }
        public float UnitValue { get; set; }
        public double Amount { get; set; }
        public double Memo { get; set; }
        public double Invoice { get; set; }
        public double Remain { get; set; }

        private double _real = 0;

        public double Real
        {
            get
            {
                _real = (Memo + Invoice) / Amount * 100;
                return _real;
            }
            set
            {
                _real = value;
            }
        }

        public string ConcurrencyStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
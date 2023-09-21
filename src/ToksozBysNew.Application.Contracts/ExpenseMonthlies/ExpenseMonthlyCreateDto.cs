using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthlyCreateDto
    {
        public string AccountId { get; set; }
        public string AccountGroup { get; set; }
        public string Account { get; set; }
        public string Department { get; set; }
        public string ExpenseType { get; set; }
        public string Product { get; set; }
        public string Proje { get; set; }
        public string Comment { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int Unit { get; set; }
        public float UnitValue { get; set; }
        public float Amount { get; set; }
        public float Memo { get; set; }
        public string Invoice { get; set; }
        public float Remain { get; set; }
    }
}
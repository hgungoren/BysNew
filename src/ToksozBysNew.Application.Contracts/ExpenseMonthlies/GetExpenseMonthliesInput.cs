using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class GetExpenseMonthliesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string AccountId { get; set; }
        public string AccountGroup { get; set; }
        public string Account { get; set; }
        public string Department { get; set; }
        public string ExpenseType { get; set; }
        public string Product { get; set; }
        public string Proje { get; set; }
        public string Comment { get; set; }
        public string Month { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public int? UnitMin { get; set; }
        public int? UnitMax { get; set; }
        public float? UnitValueMin { get; set; }
        public float? UnitValueMax { get; set; }
        public float? AmountMin { get; set; }
        public float? AmountMax { get; set; }
        public float? MemoMin { get; set; }
        public float? MemoMax { get; set; }
        public string Invoice { get; set; }
        public float? RemainMin { get; set; }
        public float? RemainMax { get; set; }

        public GetExpenseMonthliesInput()
        {

        }
    }
}
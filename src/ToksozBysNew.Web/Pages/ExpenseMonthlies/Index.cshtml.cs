using System.Threading.Tasks;
using ToksozBysNew.ExpenseMonthlies;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ToksozBysNew.Web.Pages.ExpenseMonthlies
{
    public class IndexModel : AbpPageModel
    {
        public string AccountIdFilter { get; set; }
        public string AccountGroupFilter { get; set; }
        public string AccountFilter { get; set; }
        public string DepartmentFilter { get; set; }
        public string ExpenseTypeFilter { get; set; }
        public string ProductFilter { get; set; }
        public string ProjeFilter { get; set; }
        public string CommentFilter { get; set; }
        public string MonthFilter { get; set; }
        public int? YearFilterMin { get; set; }

        public int? YearFilterMax { get; set; }
        public int? UnitFilterMin { get; set; }

        public int? UnitFilterMax { get; set; }
        public float? UnitValueFilterMin { get; set; }

        public float? UnitValueFilterMax { get; set; }
        public float? AmountFilterMin { get; set; }

        public float? AmountFilterMax { get; set; }
        public float? MemoFilterMin { get; set; }

        public float? MemoFilterMax { get; set; }
        public string InvoiceFilter { get; set; }
        public float? RemainFilterMin { get; set; }

        public float? RemainFilterMax { get; set; }

        private readonly IExpenseMonthliesAppService _expenseMonthliesAppService;

        public IndexModel(IExpenseMonthliesAppService expenseMonthliesAppService)
        {
            _expenseMonthliesAppService = expenseMonthliesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}
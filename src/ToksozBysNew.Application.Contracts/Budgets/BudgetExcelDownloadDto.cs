using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Budgets
{
    public class BudgetExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string BudgetName { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string Comment { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? OpenUntilMin { get; set; }
        public DateTime? OpenUntilMax { get; set; }
        public Guid? CompanyId { get; set; }

        public BudgetExcelDownloadDto()
        {

        }
    }
}
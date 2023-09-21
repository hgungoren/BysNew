using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Companies;

namespace ToksozBysNew.Web.Pages.Companies
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public CompanyCreateViewModel Company { get; set; }

        private readonly ICompaniesAppService _companiesAppService;

        public CreateModalModel(ICompaniesAppService companiesAppService)
        {
            _companiesAppService = companiesAppService;
        }

        public async Task OnGetAsync()
        {
            Company = new CompanyCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _companiesAppService.CreateAsync(ObjectMapper.Map<CompanyCreateViewModel, CompanyCreateDto>(Company));
            return NoContent();
        }
    }

    public class CompanyCreateViewModel : CompanyCreateDto
    {
    }
}
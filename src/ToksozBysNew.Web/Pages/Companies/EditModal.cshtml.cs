using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Companies;

namespace ToksozBysNew.Web.Pages.Companies
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CompanyUpdateViewModel Company { get; set; }

        private readonly ICompaniesAppService _companiesAppService;

        public EditModalModel(ICompaniesAppService companiesAppService)
        {
            _companiesAppService = companiesAppService;
        }

        public async Task OnGetAsync()
        {
            var company = await _companiesAppService.GetAsync(Id);
            Company = ObjectMapper.Map<CompanyDto, CompanyUpdateViewModel>(company);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _companiesAppService.UpdateAsync(Id, ObjectMapper.Map<CompanyUpdateViewModel, CompanyUpdateDto>(Company));
            return NoContent();
        }
    }

    public class CompanyUpdateViewModel : CompanyUpdateDto
    {
    }
}
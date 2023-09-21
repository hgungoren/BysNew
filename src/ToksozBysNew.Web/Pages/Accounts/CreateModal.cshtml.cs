using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Accounts;

namespace ToksozBysNew.Web.Pages.Accounts
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public AccountCreateViewModel Account { get; set; }

        private readonly IAccountsAppService _accountsAppService;

        public CreateModalModel(IAccountsAppService accountsAppService)
        {
            _accountsAppService = accountsAppService;
        }

        public async Task OnGetAsync()
        {
            Account = new AccountCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _accountsAppService.CreateAsync(ObjectMapper.Map<AccountCreateViewModel, AccountCreateDto>(Account));
            return NoContent();
        }
    }

    public class AccountCreateViewModel : AccountCreateDto
    {
    }
}
using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.AccountGroups;

namespace ToksozBysNew.Web.Pages.AccountGroups
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public AccountGroupCreateViewModel AccountGroup { get; set; }

        private readonly IAccountGroupsAppService _accountGroupsAppService;

        public CreateModalModel(IAccountGroupsAppService accountGroupsAppService)
        {
            _accountGroupsAppService = accountGroupsAppService;
        }

        public async Task OnGetAsync()
        {
            AccountGroup = new AccountGroupCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _accountGroupsAppService.CreateAsync(ObjectMapper.Map<AccountGroupCreateViewModel, AccountGroupCreateDto>(AccountGroup));
            return NoContent();
        }
    }

    public class AccountGroupCreateViewModel : AccountGroupCreateDto
    {
    }
}
using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.AccountGroups;

namespace ToksozBysNew.Web.Pages.AccountGroups
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AccountGroupUpdateViewModel AccountGroup { get; set; }

        private readonly IAccountGroupsAppService _accountGroupsAppService;

        public EditModalModel(IAccountGroupsAppService accountGroupsAppService)
        {
            _accountGroupsAppService = accountGroupsAppService;
        }

        public async Task OnGetAsync()
        {
            var accountGroup = await _accountGroupsAppService.GetAsync(Id);
            AccountGroup = ObjectMapper.Map<AccountGroupDto, AccountGroupUpdateViewModel>(accountGroup);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _accountGroupsAppService.UpdateAsync(Id, ObjectMapper.Map<AccountGroupUpdateViewModel, AccountGroupUpdateDto>(AccountGroup));
            return NoContent();
        }
    }

    public class AccountGroupUpdateViewModel : AccountGroupUpdateDto
    {
    }
}
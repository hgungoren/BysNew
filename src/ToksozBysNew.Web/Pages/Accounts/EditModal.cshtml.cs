using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Accounts;

namespace ToksozBysNew.Web.Pages.Accounts
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AccountUpdateViewModel Account { get; set; }

        private readonly IAccountsAppService _accountsAppService;

        public EditModalModel(IAccountsAppService accountsAppService)
        {
            _accountsAppService = accountsAppService;
        }

        public async Task OnGetAsync()
        {
            var account = await _accountsAppService.GetAsync(Id);
            Account = ObjectMapper.Map<AccountDto, AccountUpdateViewModel>(account);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _accountsAppService.UpdateAsync(Id, ObjectMapper.Map<AccountUpdateViewModel, AccountUpdateDto>(Account));
            return NoContent();
        }
    }

    public class AccountUpdateViewModel : AccountUpdateDto
    {
    }
}
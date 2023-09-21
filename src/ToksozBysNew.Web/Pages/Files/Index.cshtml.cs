using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ToksozBysNew.Blob;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ToksozBysNew.Web.Pages.Files
{
    public class IndexModel : AbpPageModel
    {
        [BindProperty]
        public UploadFileDto uploadFileDto { get; set; }
        private readonly IFileAppService _fileAppService;
        public bool Uploaded { get; set; } = false;
        public IndexModel(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                await uploadFileDto.File.CopyToAsync(memoryStream);
                await _fileAppService.SaveBlobAsync(new SaveBlobInputDto
                {
                    Name = uploadFileDto.Name,
                    Content = memoryStream.ToArray()
                });
            }
            return Page();
        }
        public class UploadFileDto
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile File { get; set; }
            [Required]
            [Display(Name = "Filename")]
            public string Name { get; set; }
        }
    }
}

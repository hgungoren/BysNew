﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToksozBysNew.Blob;
using Volo.Abp.AspNetCore.Mvc;

namespace ToksozBysNew.Controllers
{
    public class FileController : AbpController
    {
        private readonly IFileAppService _fileAppService;
        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }
        [HttpGet]
        [Route("download/{fileName}")]
        public async Task<IActionResult> DownloadAsync(string fileName)
        {
            var fileDto = await _fileAppService.GetBlobAsync(new GetBlobRequestDto
            {
                Name = fileName
            });
            return File(fileDto.Content, "application/octet-stream", fileDto.Name);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitleUpdateDto : IHasConcurrencyStamp
    {
        public string TitleName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
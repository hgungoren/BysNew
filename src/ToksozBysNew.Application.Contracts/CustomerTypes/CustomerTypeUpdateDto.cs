using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypeUpdateDto : IHasConcurrencyStamp
    {
        public string TypeName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
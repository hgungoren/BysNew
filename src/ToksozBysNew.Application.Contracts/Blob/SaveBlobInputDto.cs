﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToksozBysNew.Blob
{
    public class SaveBlobInputDto
    {
        public byte[] Content { get; set; }
        [Required]
        public string Name{get;set;}
}
}

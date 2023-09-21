using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressCreateDto
    {
        public string Address { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }
    }
}
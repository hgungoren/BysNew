using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Address { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public string DoctorNameSurname { get; set; }
        public string Brick { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
    }
}
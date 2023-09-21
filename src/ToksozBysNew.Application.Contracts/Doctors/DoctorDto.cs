using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Doctors
{
    public class DoctorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool IsActive { get; set; }
        public string NameSurname { get; set; }
        public string PharmacyName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? SpecId { get; set; }
        public Guid? CustomerTitleId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CustomerTypeId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public string BrickName { get; set; }
        public string Position { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string Spec { get; set; }
        public string CustomerType { get; set; }
        public string Address { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? DistrictId { get; set; }
    }
}
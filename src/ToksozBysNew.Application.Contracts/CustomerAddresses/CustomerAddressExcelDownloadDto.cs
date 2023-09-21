using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Address { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public CustomerAddressExcelDownloadDto()
        {

        }
    }
}
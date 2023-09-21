using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ToksozBysNew.CustomerAddresses
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress, Guid>
    {
        Task<CustomerAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string address = null,
            Guid? doctorId = null,
            Guid? brickId = null,
            Guid? districtId = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerAddress>> GetListAsync(
                    string filterText = null,
                    string address = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string address = null,
            Guid? doctorId = null,
            Guid? brickId = null,
            Guid? districtId = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            CancellationToken cancellationToken = default);
    }
}
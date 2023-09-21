using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Clinics
{
    public class ClinicManager : DomainService
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicManager(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public async Task<Clinic> CreateAsync(
        Guid? unitId, Guid? specId, string clinicName)
        {

            var clinic = new Clinic(
             GuidGenerator.Create(),
             unitId, specId, clinicName
             );

            return await _clinicRepository.InsertAsync(clinic);
        }

        public async Task<Clinic> UpdateAsync(
            Guid id,
            Guid? unitId, Guid? specId, string clinicName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var clinic = await _clinicRepository.GetAsync(id);

            clinic.UnitId = unitId;
            clinic.SpecId = specId;
            clinic.ClinicName = clinicName;

            clinic.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _clinicRepository.UpdateAsync(clinic);
        }

    }
}
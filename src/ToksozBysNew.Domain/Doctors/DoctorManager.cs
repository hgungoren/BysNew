using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Doctors
{
    public class DoctorManager : DomainService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorManager(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> CreateAsync(
        Guid? positionId, Guid? specId, Guid? customerTitleId, Guid? unitId, Guid? customerTypeId, bool isActive, string nameSurname, string pharmacyName)
        {

            var doctor = new Doctor(
             GuidGenerator.Create(),
             positionId, specId, customerTitleId, unitId, customerTypeId, isActive, nameSurname, pharmacyName
             );

            return await _doctorRepository.InsertAsync(doctor);
        }

        public async Task<Doctor> UpdateAsync(
            Guid id,
            Guid? positionId, Guid? specId, Guid? customerTitleId, Guid? unitId, Guid? customerTypeId, bool isActive, string nameSurname, string pharmacyName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var doctor = await _doctorRepository.GetAsync(id);

            doctor.PositionId = positionId;
            doctor.SpecId = specId;
            doctor.CustomerTitleId = customerTitleId;
            doctor.UnitId = unitId;
            doctor.CustomerTypeId = customerTypeId;
            doctor.IsActive = isActive;
            doctor.NameSurname = nameSurname;
            doctor.PharmacyName = pharmacyName;

            doctor.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _doctorRepository.UpdateAsync(doctor);
        }

    }
}
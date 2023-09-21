using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.CustomerAddresses;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Doctors;
using ToksozBysNew.Invoices;
using ToksozBysNew.Positions;
using ToksozBysNew.Specs;
using ToksozBysNew.Units;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using static ToksozBysNew.Web.Pages.Invoices.IndexModel;

namespace ToksozBysNew.Web.Pages.Doctors
{
    public class DetailModel : AbpPageModel
    {
        private readonly IDoctorsAppService _doctorsAppService;
        private readonly ISpecsAppService _specsAppService;
        private readonly IPositionRepository _positionsAppService;
        private readonly ICustomerTitlesAppService _customerTitlesAppService;
        private readonly IUnitRepository _unitRepository;
        private readonly ICustomerAddressesAppService _addressAppService;

        public DoctorViewModel Doctor { get; set; }

        public DetailModel(IDoctorsAppService doctorsAppService, ISpecsAppService specsAppService, IPositionRepository positionsAppService,ICustomerTitlesAppService customerTitlesAppService, IUnitRepository unitRepository, ICustomerAddressesAppService addressRepository)
        {
            _doctorsAppService = doctorsAppService;
            _specsAppService = specsAppService;
            _positionsAppService = positionsAppService;
            _customerTitlesAppService = customerTitlesAppService;
            _unitRepository = unitRepository;
            _addressAppService = addressRepository;
        }
        public async Task OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await Task.CompletedTask;
            }
            else
            {
                var doctor = await _doctorsAppService.GetAsync(id); 
                var specId= Guid.Parse(doctor.SpecId.ToString());
                var spec = await _doctorsAppService.GetSpecById(specId);
                var posId= Guid.Parse(doctor.PositionId.ToString());
                var position =await _positionsAppService.GetAsync(posId);
                var titleId = Guid.Parse(doctor.CustomerTitleId.ToString());
                var title = await _customerTitlesAppService.GetAsync(titleId);
                var unitId = Guid.Parse(doctor.UnitId.ToString());
                var unit = await _unitRepository.GetAsync(unitId);
                var dataWithList = await _doctorsAppService.GetListWithBrickNameByDoctorIdAsync(id);
                //var dataWithList1 = await _addressAppService.GetListWithDoctorIdAsync(id);
                var data = dataWithList.Items.FirstOrDefault();

                Doctor = ObjectMapper.Map<DoctorDto, DoctorViewModel>(data);

            }
        }
        public class DoctorViewModel : DoctorDto { 
        }
        public class AddressViewModel : CustomerAddressDto
        {
        }
    }
}

using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.Data.Professionals;

namespace scheapp.app.DataServices
{
    public class ProfessionalDataService : IProfessionalDataService
    {
        private readonly ILogger _logger;
        private readonly IApiHelper _apiHelper;
        public ProfessionalDataService(ILogger<ProfessionalDataService> logger, IApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async Task<List<Professional>> GetProfessionals() => await _apiHelper.CallGetApi<List<Professional>>("/Professional/GetProfessionals");
        public async Task SaveProfessionals(Professional professional) => await _apiHelper.CallPostApi<Professional>("/Professional/SaveProfessionals", professional);
        public async Task<List<ProfessionalSchedule>> GetProfessionalSchedules() => await _apiHelper.CallGetApi<List<ProfessionalSchedule>>("/Professional/GetProfessionalSchedules");
        public async Task SaveProfessionalSchedules(ProfessionalSchedule professional) => await _apiHelper.CallPostApi<ProfessionalSchedule>("/Professional/SaveProfessionalSchedules", professional);
        public async Task<List<ProfessionalScheduleAppointmentRequest>> GetProfessionalScheduleAppointmentRequests() => await _apiHelper.CallGetApi<List<ProfessionalScheduleAppointmentRequest>>("/Professional/GetProfessionalScheduleAppointmentRequests");
        public async Task SaveProfessionalScheduleAppointmentRequests(ProfessionalScheduleAppointmentRequest professional) => await _apiHelper.CallPostApi<ProfessionalScheduleAppointmentRequest>("/Professional/SaveProfessionalScheduleAppointmentRequests", professional);
        public async Task<List<ProfessionalService>> GetProfessionalServices() => await _apiHelper.CallGetApi<List<ProfessionalService>>("/Professional/GetProfessionalServices");
        public async Task SaveProfessionalServices(ProfessionalService professional) => await _apiHelper.CallPostApi<ProfessionalService>("/Professional/SaveProfessionalServices", professional);
    }
}

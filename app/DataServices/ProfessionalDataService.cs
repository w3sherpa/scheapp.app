﻿using scheapp.app.DataServices.Interfaces;
using scheapp.app.Helpers;
using scheapp.app.Models.API;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.Data.TableModels.Businesses;
using scheapp.app.Models.Data.TableModels.Professionals;

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

        public async Task<List<Professional>> GetProfessionals(int businessId) => await _apiHelper.CallGetApi<List<Professional>>($"/Professional/GetProfessionals?businessId={businessId}");
        public async Task<HttpResponseMessage> SaveProfessionals(Professional professional) => await _apiHelper.CallPostApi<Professional>("/Professional/SaveProfessionals", professional);
        public async Task<List<ProfessionalSchedule>> GetProfessionalSchedules(int businessId,int? professionalId) => await _apiHelper.CallGetApi<List<ProfessionalSchedule>>($"/Professional/GetProfessionalSchedules?businessId={businessId}&professionalId={professionalId}");
        public async Task<HttpResponseMessage> SaveProfessionalSchedules(ProfessionalSchedule professional) => await _apiHelper.CallPostApi<ProfessionalSchedule>("/Professional/SaveProfessionalSchedules", professional);
        public async Task<HttpResponseMessage> DeleteProfessionalSchedules(DeleteProfessionalScheduleRQ deleteSchedules) => await _apiHelper.CallPostApi<DeleteProfessionalScheduleRQ>("/Professional/DeleteProfessionalSchedules", deleteSchedules);
        public async Task<List<ProfessionalScheduleAppointmentRequest>> GetProfessionalScheduleAppointmentRequests() => await _apiHelper.CallGetApi<List<ProfessionalScheduleAppointmentRequest>>("/Professional/GetProfessionalScheduleAppointmentRequests");
        public async Task SaveProfessionalScheduleAppointmentRequests(ProfessionalScheduleAppointmentRequest professional) => await _apiHelper.CallPostApi<ProfessionalScheduleAppointmentRequest>("/Professional/SaveProfessionalScheduleAppointmentRequests", professional);
        //public async Task<List<ProfessionalService>> GetProfessionalServices() => await _apiHelper.CallGetApi<List<ProfessionalService>>("/Professional/GetProfessionalServices");
        public async Task SaveProfessionalServices(ProfessionalService professional) => await _apiHelper.CallPostApi<ProfessionalService>("/Professional/SaveProfessionalServices", professional);

        public async Task<List<ProfessionalScheduleAppointmentRequestsDetailDsp>> GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(int businessId, DateOnly? date)
        {
            return await _apiHelper.CallGetApi<List<ProfessionalScheduleAppointmentRequestsDetailDsp>>($"/Professional/GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId?businessId={businessId}&date={date}");
        } 
        public async Task<List<ProfessionalBusinessDetailDsp>> GetProfessionalBusinessDetailDsp(int? professionaId, int? businessId)
        {
            return await _apiHelper.CallGetApi<List<ProfessionalBusinessDetailDsp>>($"/Professional/GetProfessionalBusinessDetails?professionalId={professionaId}&businessId={businessId}");
        }
    }
}

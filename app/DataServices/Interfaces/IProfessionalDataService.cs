using scheapp.app.Models.API;
using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.Data.TableModels.Professionals;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IProfessionalDataService
    {
        Task<List<Professional>> GetProfessionals(int businessId);
        Task<List<ProfessionalScheduleAppointmentRequest>> GetProfessionalScheduleAppointmentRequests();
        Task<List<ProfessionalSchedule>> GetProfessionalSchedules(int businessId, int? professionalId);
        //Task<List<ProfessionalService>> GetProfessionalServices();
        Task<HttpResponseMessage> SaveProfessionals(Professional professional);
        Task<HttpResponseMessage> DeleteProfessionalSchedules(DeleteProfessionalScheduleRQ deleteSchedules);
        Task SaveProfessionalScheduleAppointmentRequests(ProfessionalScheduleAppointmentRequest professional);
        Task SaveProfessionalSchedules(ProfessionalSchedule professional);
        Task SaveProfessionalServices(ProfessionalService professional);
        Task<List<ProfessionalScheduleAppointmentRequestsDetailDsp>> GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(int businessId, DateOnly? date);
        Task<List<ProfessionalBusinessDetailDsp>> GetProfessionalBusinessDetailDsp(int? professionaId, int? businessId);
    }
}
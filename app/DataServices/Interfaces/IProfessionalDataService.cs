using scheapp.app.Models.Data.DspModels;
using scheapp.app.Models.Data.TableModels.Professionals;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IProfessionalDataService
    {
        Task<List<Professional>> GetProfessionals();
        Task<List<ProfessionalScheduleAppointmentRequest>> GetProfessionalScheduleAppointmentRequests();
        Task<List<ProfessionalSchedule>> GetProfessionalSchedulesByBusinessId(int businessId);
        Task<List<ProfessionalService>> GetProfessionalServices();
        Task SaveProfessionals(Professional professional);
        Task SaveProfessionalScheduleAppointmentRequests(ProfessionalScheduleAppointmentRequest professional);
        Task SaveProfessionalSchedules(ProfessionalSchedule professional);
        Task SaveProfessionalServices(ProfessionalService professional);
        Task<List<ProfessionalScheduleAppointmentRequestsDetailDsp>> GetProfessionalScheduleAppointmentRequestsDetailsByBusinessId(int businessId, DateOnly? date);
        Task<List<ProfessionalBusinessDetailDsp>> GetProfessionalBusinessDetailDsp(int? professionaId, int? businessId);
    }
}
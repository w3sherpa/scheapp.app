using scheapp.app.Models.Data.Professionals;

namespace scheapp.app.DataServices.Interfaces
{
    public interface IProfessionalDataService
    {
        Task<List<Professional>> GetProfessionals();
        Task<List<ProfessionalScheduleAppointmentRequest>> GetProfessionalScheduleAppointmentRequests();
        Task<List<ProfessionalSchedule>> GetProfessionalSchedules();
        Task<List<ProfessionalService>> GetProfessionalServices();
        Task SaveProfessionals(Professional professional);
        Task SaveProfessionalScheduleAppointmentRequests(ProfessionalScheduleAppointmentRequest professional);
        Task SaveProfessionalSchedules(ProfessionalSchedule professional);
        Task SaveProfessionalServices(ProfessionalService professional);
    }
}
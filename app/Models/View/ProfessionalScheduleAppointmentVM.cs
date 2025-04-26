namespace scheapp.app.Models.View
{
    public class AppointmentsVM
    {
        public int BusinessId { get; set; }
        public List<ProfessionalScheduleAppointmentVM> Appointments { get; set; } = new();
        public bool IsTodaySelected { get; set; }
        public bool IsAllDateSelected { get; set; }
        public bool IsDateSelected { get; set; }
        public string SelectedDate { get; set; }
    }
    public class ProfessionalScheduleAppointmentVM
    {
        public int ScheduleAppointmentId { get; set; }
        public string StartDT { get; set; } = "";
        public string EndDT { get; set; } = "";
        public bool ProfessionalConfirmed { get; set; }
        public bool CustomerConfirmed { get; set; }
        public string RequestDate { get; set; } = "";
        public string Professional { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string Customer { get; set; } = "";
    }
}

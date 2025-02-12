namespace scheapp.app.Models.View
{
    public class AppointmentsVM
    {
        public List<ProfessionalScheduleAppointmentVM> Appointments { get; set; } = new();
        public bool IsTodaySelected { get; set; }
        public bool IsAllDateSelected { get; set; }
        public bool IsDateSelected { get; set; }
        public string SelectedDate { get; set; }
    }
    public class ProfessionalScheduleAppointmentVM
    {
        public int ScheduleAppointmentId { get; set; }
        public DateTime StartDT { get; set; } = new();
        public DateTime EndDT { get; set; } = new();
        public bool ProfessionalConfirmed { get; set; }
        public bool CustomerConfirmed { get; set; }
        public DateTime RequestDate { get; set; } = new();
        public string Professional { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string Customer { get; set; } = "";
    }
}

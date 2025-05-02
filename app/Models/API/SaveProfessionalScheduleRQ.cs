namespace scheapp.app.Models.API
{
    public class SaveProfessionalScheduleRQ
    {
        public int ProfessionalId { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string DaysOfWeek { get; set; }
        public int BusinessId { get; set; }
    }
}

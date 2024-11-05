namespace scheapp.app.Models.Data
{
    public class RequestByBusinessId
    {
        public int BusinessId { get; set; }
    }
    public class RequestByProfessionalId
    {
        public int ProfessionalId { get; set; }
    }
    public class RequestByServiceId
    {
        public int ProfessionalId { get; set; }
    }
    public class RequestByDate
    {
        public string DateOnly { get; set; } = "";
    }

    public class TwilioStatusCallback
    {
        public string Called { get; set; } = "";
        public string ToState { get; set; } = "";
        public string CallerCountry { get; set; } = "";
        public string Direction { get; set; } = "";
        public string Timestamp { get; set; } = "";
        public string CallbackSource { get; set; } = "";
        public string CallerState { get; set; } = "";
        public string ToZip { get; set; } = "";
        public string SequenceNumber { get; set; } = "";
        public string StirStatus { get; set; } = "";
        public string CallSid { get; set; } = "";
        public string To { get; set; } = "";
        public string CallerZip { get; set; } = "";
        public string ToCountry { get; set; } = "";
        public string CalledZip { get; set; } = "";
        public string ApiVersion { get; set; } = "";
        public string CalledCity { get; set; } = "";
        public string CallStatus { get; set; } = "";
        public string From { get; set; } = "";
        public string AccountSid { get; set; } = "";
        public string CalledCountry { get; set; } = "";
        public string CallerCity { get; set; } = "";
        public string ToCity { get; set; } = "";
        public string FromCountry { get; set; } = "";
        public string Caller { get; set; } = "";
        public string FromCity { get; set; } = "";
        public string CalledState { get; set; } = "";
        public string FromZip { get; set; } = "";
        public string FromState { get; set; } = "";
    }

    public class DeleteProfessionalScheduleRQ
    {
        public int? BusinessId { get; set; }
        public int? ProfessionalScheduleId { get; set; }

    }
}
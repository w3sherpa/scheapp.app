namespace scheapp.app.Models.API;

public class SaveCustomerDspResult
{
    public int CustomerId { get; set; }
    public int CustomerContactId { get; set; }
}

public class ProfessionalDsp
{
    public int ProfessionalId { get; set; }
    public string FullName { get; set; } = string.Empty;
}

public class ProfessionalServiceDsp
{
    public int ProfessionalId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ServiceDuration { get; set; }
    public string DurationType { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class ProfessionalScheduleAppointmentRequestsDetailDsp
{
    public int ScheduleAppointmentId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
    public bool ProfessionalConfirmed { get; set; }
    public bool CustomerConfirmed { get; set; }
    public DateTime RequestDate { get; set; }
    public string ProFirst { get; set; } = string.Empty;
    public string? ProMiddle { get; set; }
    public string ProLast { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string CustFirst { get; set; } = string.Empty;
    public string? CustMiddle { get; set; }
    public string CustLast { get; set; } = string.Empty;
}

public class ProfessionalBusinessDetailDsp
{
    public string AspNetUserId { get; set; } = string.Empty;
    public string? AspNetUserName { get; set; }
    public int ProfessionalId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? ProfessionalRole { get; set; }
    public int BusinessId { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string BusinessWebsite { get; set; } = string.Empty;
}

public class ProfessionalScheduledAppointmentsDsp
{
    public int Id { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
}

public class BookingConflictDsp
{
    public string ConflictType { get; set; } = string.Empty;
    public int ConflictId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
}

public class AvailableSlotDsp
{
    public int ProfessionalId { get; set; }
    public string ProfessionalName { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public DateTime ServiceDate { get; set; }
    public DateTime SlotStartDT { get; set; }
    public DateTime SlotEndDT { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
}

public class CreateAppointmentResultDsp
{
    public int AppointmentId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
}

public class CreateAppointmentHoldResultDsp
{
    public int HoldId { get; set; }
    public string HoldToken { get; set; } = string.Empty;
}

public class ConfirmAppointmentHoldResultDsp
{
    public int AppointmentId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
}

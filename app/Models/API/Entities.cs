namespace scheapp.app.Models.API;

public class Business
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string StreetOne { get; set; } = string.Empty;
    public string? StreetTwo { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal? Lat { get; set; }
    public decimal? Long { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ContactType
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public int? BusinessId { get; set; }
}

public class CustomerContact
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int TypeId { get; set; }
    public string Contact { get; set; } = string.Empty;
    public DateTime? CreatedDT { get; set; }
}

public class Professional
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public int? BusinessId { get; set; }
    public string AspNetUserId { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Gender { get; set; }
    public string? AspNetUserName { get; set; }
    public string? Email { get; set; }
    public string? ProfessionalRole { get; set; }
}

public class ProfessionalContact
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public int TypeId { get; set; }
    public string Contact { get; set; } = string.Empty;
}

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? BusinessId { get; set; }
}

public class ServiceDuration
{
    public int Id { get; set; }
    public int Duration { get; set; }
    public string DurationType { get; set; } = string.Empty;
    public int BusinessId { get; set; }
}

public class ProfessionalService
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public int ServiceId { get; set; }
    public int ServiceDurationId { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public Professional? Professional { get; set; }
}

public class ProfessionalSchedule
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? DaysOfWeek { get; set; }
    public int? BusinessId { get; set; }
    public bool IsDeleted { get; set; }
}

public class ProfessionalScheduleAppointmentRequest
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public int CustomerId { get; set; }
    public int ServiceId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
    public bool ProfessionalConfirmed { get; set; } = true;
    public bool CustomerConfirmed { get; set; } = true;
    public int? BusinessId { get; set; }
    public DateTime DTStamp { get; set; }
}

public class CustomerCall
{
    public int Id { get; set; }
    public int CostomerContactId { get; set; }
    public int ScheduleAppointmentRequestId { get; set; }
    public DateTime CalledAt { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Status { get; set; }
    public int BusinessId { get; set; }
    public string? VoiceApiConversationId { get; set; }
    public string? KeyPressed { get; set; }
}

public class ProfessionalBlockedTime
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public int BusinessId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
    public string? Reason { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProfessionalScheduleOverride
{
    public int Id { get; set; }
    public int ProfessionalId { get; set; }
    public int BusinessId { get; set; }
    public DateOnly OverrideDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsAvailable { get; set; }
    public string? Reason { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AppointmentHold
{
    public int Id { get; set; }
    public int BusinessId { get; set; }
    public int ProfessionalId { get; set; }
    public int ServiceId { get; set; }
    public int? CustomerId { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime EndDT { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string HoldToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class ProfessionalScheduleDay
{
    public int ProfessionalScheduleId { get; set; }
    public byte DayOfWeek { get; set; }
}

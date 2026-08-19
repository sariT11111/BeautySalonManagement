namespace BeautySalon.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Method { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public Appointment? Appointment { get; set; } 
}
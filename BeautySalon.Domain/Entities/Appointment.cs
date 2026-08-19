namespace BeautySalon.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "Scheduled";

    public string? Notes { get; set; }

    public Customer Customer { get; set; } = null!;

    public Staff Staff { get; set; } = null!;

    public ICollection<AppointmentService> AppointmentServices { get; set; }
        = new List<AppointmentService>();

    public Payment? Payment { get; set; }
}
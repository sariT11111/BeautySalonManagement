namespace BeautySalon.Domain.Entities;

public class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int DurationMinutes { get; set; }

    public string Category { get; set; } = string.Empty;

    public ICollection<AppointmentService> AppointmentServices { get; set; }
        = new List<AppointmentService>();
}
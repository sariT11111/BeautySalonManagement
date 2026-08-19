namespace BeautySalon.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();
}
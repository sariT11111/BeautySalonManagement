namespace BeautySalon.Domain.Entities;

public class Staff
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();
}
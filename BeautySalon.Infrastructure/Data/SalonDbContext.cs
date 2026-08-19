using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Data;

public class SalonDbContext : DbContext
{
    public SalonDbContext(DbContextOptions<SalonDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Staff> Staff => Set<Staff>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentService> AppointmentServices => Set<AppointmentService>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AppointmentService composite primary key
        modelBuilder.Entity<AppointmentService>()
            .HasKey(x => new { x.AppointmentId, x.ServiceId });

        // Appointment → Customer
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment → Staff
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Staff)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        // AppointmentService → Appointment
        modelBuilder.Entity<AppointmentService>()
            .HasOne(x => x.Appointment)
            .WithMany(x => x.AppointmentServices)
            .HasForeignKey(x => x.AppointmentId);

        // AppointmentService → Service
        modelBuilder.Entity<AppointmentService>()
            .HasOne(x => x.Service)
            .WithMany(x => x.AppointmentServices)
            .HasForeignKey(x => x.ServiceId);

        // Appointment → Payment (one-to-one)
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Appointment)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(x => x.AppointmentId);
    }
}
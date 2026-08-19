using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly SalonDbContext _context;

    public AppointmentsController(SalonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return await _context.Appointments
            .Include(a => a.Customer)
            .Include(a => a.Staff)
            .Include(a => a.AppointmentServices)
                .ThenInclude(x => x.Service)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Customer)
            .Include(a => a.Staff)
            .Include(a => a.AppointmentServices)
                .ThenInclude(x => x.Service)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment is null)
            return NotFound();

        return appointment;
    }

    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment(
        Appointment appointment)
    {
        var customerExists = await _context.Customers
            .AnyAsync(c => c.Id == appointment.CustomerId);

        if (!customerExists)
            return BadRequest("Customer does not exist.");

        var staffExists = await _context.Staff
            .AnyAsync(s => s.Id == appointment.StaffId);

        if (!staffExists)
            return BadRequest("Staff member does not exist.");

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAppointment),
            new { id = appointment.Id },
            appointment);
    }
}
using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentServicesController : ControllerBase
{
    private readonly SalonDbContext _context;

    public AppointmentServicesController(SalonDbContext context)
    {
        _context = context;
    }

   [HttpGet]
public async Task<ActionResult> GetAppointmentServices()
{
    var services = await _context.AppointmentServices
        .Select(x => new
        {
            x.AppointmentId,
            x.ServiceId,
            x.Price,
            ServiceName = x.Service!.Name,
            AppointmentDate = x.Appointment!.AppointmentDate
        })
        .ToListAsync();

    return Ok(services);
}
    [HttpPost]
    public async Task<ActionResult<AppointmentService>> AddService(
        AppointmentService appointmentService)
    {
        var appointmentExists = await _context.Appointments
            .AnyAsync(a => a.Id == appointmentService.AppointmentId);

        if (!appointmentExists)
            return BadRequest("Appointment does not exist.");

        var serviceExists = await _context.Services
            .AnyAsync(s => s.Id == appointmentService.ServiceId);

        if (!serviceExists)
            return BadRequest("Service does not exist.");

        _context.AppointmentServices.Add(appointmentService);
        await _context.SaveChangesAsync();

        return Ok(new
{
    appointmentService.AppointmentId,
    appointmentService.ServiceId,
    appointmentService.Price
});
    }
}
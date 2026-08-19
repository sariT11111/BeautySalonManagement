using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly SalonDbContext _context;

    public PaymentsController(SalonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        return await _context.Payments
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);

        if (payment == null)
            return NotFound();

        return payment;
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
    {
        var appointmentExists = await _context.Appointments
            .AnyAsync(a => a.Id == payment.AppointmentId);

        if (!appointmentExists)
            return BadRequest("Appointment does not exist.");

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPayment),
            new { id = payment.Id },
            payment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, Payment payment)
    {
        if (id != payment.Id)
            return BadRequest();

        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);

        if (payment == null)
            return NotFound();

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
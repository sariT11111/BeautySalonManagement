using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly SalonDbContext _context;

    public StaffController(SalonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
    {
        return await _context.Staff.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Staff>> GetStaffMember(int id)
    {
        var staff = await _context.Staff.FindAsync(id);

        if (staff is null)
            return NotFound();

        return staff;
    }

    [HttpPost]
    public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
    {
        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetStaffMember),
            new { id = staff.Id },
            staff);
    }
}
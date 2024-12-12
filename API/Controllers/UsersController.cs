using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks; // For async/await

namespace API;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync(); // Async database call
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Log the exception (use a logging framework)
            return StatusCode(500, "An error occurred while fetching users.");
        }
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            // Log the exception (use a logging framework)
            return StatusCode(500, "An error occurred while fetching the user.");
        }
    }
}

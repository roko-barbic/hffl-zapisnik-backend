using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;
using roko_test.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;
using BCrypt.Net;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly DataContext _context;

    public LoginController(DataContext context)
    {
        _context = context;
    }

    // [HttpPost]
    // public async Task<IActionResult> RegisterAsync([FromBody] LoginModel registerModel)
    // {
    //     // Check if the username is already taken
    //     if (await _context.Users.AnyAsync(u => u.Username == registerModel.Username))
    //     {
    //         return Conflict(new { message = "Username is already taken." });
    //     }

    //     // Hash the password using BCrypt
    //     string hashedPassword = BCrypt.HashPassword(registerModel.Password);

    //     // Create a new user
    //     var user = new User
    //     {
    //         Username = registerModel.Username,
    //         PasswordHash = hashedPassword,
    //     };

    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();

    //     return Ok(new { message = "User registered successfully." });
    // }
    
    
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        // Find the user by username
        var user = await _context.Admins.SingleOrDefaultAsync(u => u.Username == loginModel.Username);

        if (user == null)
        {
            return Unauthorized(); // User not found
        }

        // Verify the provided password against the stored hash using BCrypt
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash);

        if (passwordMatch)
        {
            // Authentication successful
            return Ok(new { message = "Authentication successful" });
        }

        // Authentication failed
        return Unauthorized();
    }
   
}

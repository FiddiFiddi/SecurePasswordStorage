using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Entities;
using Entities.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeleteThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HelpContext _context;

        public AuthController(HelpContext context)
        {
            _context = context;
        }

        // Asks DAL If to validate user input
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userFound = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            var isValidated = CryptoManager.CheckPassword(user.Password, userFound.Password);
            if (isValidated)
            {
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("create/user")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest();

            var hashedPassword = CryptoManager.HashPassword(user.Password);
            user.Password = hashedPassword;

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
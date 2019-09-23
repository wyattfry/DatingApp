using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository repo;
        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto user)
        {
            // validate request
            user.Username = user.Username.ToLower();

            if (await this.repo.UserExists(user.Username))
            {
                return BadRequest($"Username '{user.Username}' already exists.");
            }

            var userToCreate = new User
            {
                Username = user.Username,
            };

            var createdUser = await this.repo.Register(userToCreate, user.Password);

            // TODO Replace with CreatedAtRoute once get user feature exists
            return StatusCode(201);
        }
    }
}
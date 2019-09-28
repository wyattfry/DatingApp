using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Dtos;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository repo;

        public IConfiguration config { get; }

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var userFromRepo = await this.repo.Login(user.Username.ToLower(), user.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            // build token to return to user
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
            };
            // server must sign the token to make it valid
            var appToken = this.config.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appToken));

            // generate signing creds
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // create security token descriptor with exp date etc
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
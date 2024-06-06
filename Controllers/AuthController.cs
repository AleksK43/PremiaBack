using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Premia_API.Data;
using Premia_API.DTO;
using Premia_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using System.Security.Claims;
using System.Text;


namespace Premia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        public AuthController(IConfiguration congifuration)
        {
            _configuration = congifuration;
        }


        [HttpPost("register")]
        public ActionResult<UserLoginDTO> Register(UserRegisterTaskDTO request)
        {   
         
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == request.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.Department = request.Deprtment;
            user.SupervisorId = request.SupervisorId;
            user.Email = request.Email;
           
            user.PasswordHash = passwordHash;
            user.Username = string.Concat(request.Name, request.LastName);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<UserLoginDTO> Login(UserLoginDTO request)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return BadRequest("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
            {
                return BadRequest("Invalid password");
            }

            string token = CreateToken(existingUser);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("isNormalUser", user.isNormalUser.ToString()),
                new Claim("isSuperUser", user.isSuperUser.ToString()),
                new Claim("isSupervisor", user.isSupervisor.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

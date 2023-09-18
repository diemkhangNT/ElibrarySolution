using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyNguoiDung.Configuations;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Dto;
using QuanLyNguoiDung.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationUsersController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationUsersController(UserDBContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDto login)
        {
            var user = _context.GiangViens.SingleOrDefault(p=> p.Username == login.Username 
            && p.Password == login.Password);
            if(user == null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Message = new List<string>()
                    {
                        "Invalid username/password"
                    }
                });
            }
            return Ok(new AuthResult()
            {
                Result = true,
                Message = new List<string>()
                    {
                        "User valid!", 
                        "Authentication success"
                    },
                Token = GenarateJwtToken(user)
            });
        }


        private string GenarateJwtToken(GiangVien user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

            //description
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("MaGV", user.MaGV),
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Name, user.TenGV),
                    //new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                    new Claim("TockenId", Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token); 
        }
    }
}

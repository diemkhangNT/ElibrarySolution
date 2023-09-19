//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using NuGet.Common;
//using QuanLyNguoiDung.Configuations;
//using QuanLyNguoiDung.Dto;
//using QuanLyNguoiDung.Model;
//using System.Configuration;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace QuanLyNguoiDung.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticationsController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        //private readonly JwtConfig _jwtConfig;
//        private readonly IConfiguration _configuration;
//        public AuthenticationsController(UserManager<IdentityUser> userManager,
//            IConfiguration configuration
//            //JwtConfig jwtConfig

//            )
//        {
//            _userManager = userManager;
//            //_jwtConfig = jwtConfig;
//            _configuration = configuration;
//        }

//        [HttpPost]
//        [Route("Register")]
//        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
//        {
//            if(ModelState.IsValid)
//            {
//                var userExist = await _userManager.FindByEmailAsync(registrationDto.Email);
//                if(userExist != null)
//                {
//                    return BadRequest(new AuthResult()
//                    {
//                        Result = false,
//                        Message = new List<string>()
//                        {
//                            "Email already exist"
//                        }
//                    });
//                }

//                var newUser = new IdentityUser()
//                {
//                    Email = registrationDto.Email,
//                    UserName = registrationDto.Email
//                };

//                var isCreated = await _userManager.CreateAsync(newUser, registrationDto.Password);

//                if (isCreated.Succeeded)
//                {
//                    var token = GenarateJwtToken(newUser);
//                    return Ok(new AuthResult()
//                    {
//                        Result = true,
//                        Token = token
//                    });

//                }
//                return BadRequest(new AuthResult()
//                {
//                    Result= false,
//                    Message = new List<string>()
//                    {
//                        "Server error"
//                    }
//                });
//            }
//            return BadRequest();
//        }

//        [Route("Login")]
//        [HttpPost]
//        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
//        {
//            if (ModelState.IsValid)
//            {
//                var existUser = await _userManager.FindByEmailAsync(userLogin.Email);

//                if (existUser == null)
//                {
//                    return BadRequest(new AuthResult()
//                    {
//                        Result = false,
//                        Message = new List<string>()
//                        {
//                            "Invalid Payload"
//                        }
//                    });
//                }
//                var isCorrect = await _userManager.CheckPasswordAsync(existUser, userLogin.Password);

//                if (!isCorrect)
//                {
//                    return BadRequest(new AuthResult()
//                    {
//                        Result = false,
//                        Message = new List<string>()
//                        {
//                            "Invalid credentials"
//                        }
//                    });
//                }

//                var jwtToken = GenarateJwtToken(existUser);
//                return Ok(new AuthResult()
//                {
//                    Result = true,
//                    Token = jwtToken
//                });
//            }
//            return BadRequest(new AuthResult()
//            {
//                Result = false,
//                Message = new List<string>()
//                {
//                    "Invalid payload"
//                }
//                }) ;
//        }


//        private string GenarateJwtToken(IdentityUser user)
//        {
//            var jwtTokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

//            //description
//            var tokenDescription = new SecurityTokenDescriptor()
//            {
//                Subject = new ClaimsIdentity(new[]
//                {
//                    new Claim("Id", user.Id),
//                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
//                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
//                }),

//                Expires = DateTime.UtcNow.AddMinutes(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
//            };

//            var token = jwtTokenHandler.CreateToken(tokenDescription);
//            var jwtToken = jwtTokenHandler.WriteToken(token);
//            return jwtToken;
//        }
//    }
//}

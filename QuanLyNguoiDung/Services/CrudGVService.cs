using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyNguoiDung.Configuations;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyNguoiDung.Services
{
    public class CrudGVService : ICrudGVService
    {
        private readonly UserDBContext _context;
        private readonly JwtConfig _jwtConfig;

        public CrudGVService(UserDBContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<bool> Delete_GiangVien(string maGV)
        {
            if (_context.GiangViens == null)
            {
                return false;
            }
            var giangVien = await _context.GiangViens.FindAsync(maGV);
            if (giangVien == null)
            {
                return false;
            }
            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GiangVien> Get_GiangVien(string maGV)
        {
            var giangVien = await _context.GiangViens.FindAsync(maGV);
            return giangVien;
        }

        public async Task<IEnumerable<GiangVien>> Get_GiangViens()
        {
            return await _context.GiangViens.ToListAsync();
        }

        public async Task<GiangVien> Post_GiangVien(GiangVien giangVien)
        {
            _context.GiangViens.Add(giangVien);
            await _context.SaveChangesAsync();
            return giangVien;
        }

        public async Task<GiangVien> Put_GiangVien(GiangVien giangVien)
        {
            _context.Entry(giangVien).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return giangVien;
        }

        public string GenarateJwtToken(GiangVien user)
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

                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyNguoiDung.Configuations;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyNguoiDung.Services
{
    public class CrudHVService : ICrudHVService
    {
        public readonly UserDBContext _context;
        private readonly JwtConfig _jwtConfig;

        public CrudHVService(UserDBContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<bool> Delete_HocVien(string maHV)
        {
            if (_context.HocViens == null)
            {
                return false;
            }
            var hocVien = await _context.HocViens.FindAsync(maHV);
            if (hocVien == null)
            {
                return false;
            }
            _context.HocViens.Remove(hocVien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HocVien>> Get_HocViens()
        {
            return await _context.HocViens.ToListAsync();
        }

        public async Task<HocVien> Get_HovVien(string maHV)
        {
            var hocVien = await _context.HocViens.FindAsync(maHV);
            return hocVien;
        }

        public async Task<HocVien> Post_HocVien(HocVien hocVien)
        {
            _context.HocViens.Add(hocVien);
            await _context.SaveChangesAsync();
            return hocVien;
        }

        public async Task<HocVien> Put_HocVien(HocVien hocVien)
        {
            _context.Entry(hocVien).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hocVien;
        }
        public string GenarateJwtToken(HocVien user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

            //description
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("MaHV", user.MaHV),
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Name, "HocVien"),
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

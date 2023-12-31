﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyNguoiDung.Configuations;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyNguoiDung.Services
{
    public class CrudLDService : ICrudLDService
    {
        private readonly UserDBContext _context;
        private readonly JwtConfig _jwtConfig;

        public CrudLDService(UserDBContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<bool> Delete_Leadership(string maLD)
        {
            if (_context.leaderships == null)
            {
                return false;
            }
            var lds = await _context.leaderships.FindAsync(maLD);
            if (lds == null)
            {
                return false;
            }
            _context.leaderships.Remove(lds);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Leadership> Get_Leadership(string maLD)
        {
            var leadership = await _context.leaderships.FindAsync(maLD);
            return leadership;
        }

        public async Task<IEnumerable<Leadership>> Get_Leaderships()
        {
            return await _context.leaderships.ToListAsync();
        }

        public async Task<Leadership> Post_Leadership(Leadership leadership)
        {
            _context.leaderships.Add(leadership);
            await _context.SaveChangesAsync();
            return leadership;
        }

        public async Task<Leadership> Put_Leadership(Leadership leadership)
        {
            _context.Entry(leadership).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return leadership;
        }

        public async Task<TokenModel> GenarateJwtToken(Leadership user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

            //description
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("MaGV", user.MaLD),
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Name, user.TenLD),
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
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            //lưu database
            var refreshTokeEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserID = user.MaLD.ToString(),
                JwtID = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddHours(1)
            };
            await _context.refreshTokens.AddAsync(refreshTokeEntity);
            await _context.SaveChangesAsync();
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }
    }
}

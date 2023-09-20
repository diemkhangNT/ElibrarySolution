using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyNguoiDung.Configuations;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace QuanLyNguoiDung.Services
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly UserDBContext _context;
        private readonly JwtConfig _jwtConfig;
        private readonly ICrudGVService _crudServiceGV;
        private readonly ICrudHVService _crudServiceHV;
        private readonly ICrudLDService _crudServiceLD;

        public RefreshTokenService(UserDBContext context, IOptionsMonitor<JwtConfig> optionsMonitor, ICrudGVService crudServiceGV, ICrudHVService crudServiceHV, ICrudLDService crudServiceLD)
        {
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;
            _crudServiceGV = crudServiceGV;
            _crudServiceHV = crudServiceHV;
            _crudServiceLD = crudServiceLD;
        }

        public bool CheckRefreshToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                //cài đặt thời gian token expire
                //RequireExpirationTime = true,
                ValidateLifetime = false, //không kiểm tra token hết hạn
                ClockSkew = TimeSpan.Zero

            };
            try
            {
                //check 1: access token vavid
                var tokenInverification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidationParameters, out var validatedToken);

                //check 2: check thuật toán
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if(!result)
                    {
                        return false;
                    }
                }

                //check 3: check accesstoken expire
                var utcexpireDate = long.Parse(tokenInverification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDateTime(utcexpireDate);
                if((DateTime)expireDate > DateTime.Now)
                {
                    return false;
                }

                //check 4: check exist in DB
                var storedToken = _context.refreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if(storedToken is null)
                {
                    return false;
                }

                //check 5: check refresh token is used/revoked?
                if(storedToken.IsUsed)
                {
                    return false;
                }
                if(storedToken.IsRevoked)
                {
                    return false;
                }

                //check 6: Accesstoken ID == JwtID in Refreshtoken?
                var jti = tokenInverification.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Jti).Value;
                if(storedToken.JwtID != jti)
                {
                    return false;
                }

                //update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _context.refreshTokens.Update(storedToken);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private object ConvertUnixTimeToDateTime(long utcexpireDate)
        {
            var datetimeInterval = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Local);
            datetimeInterval.AddSeconds(utcexpireDate).ToUniversalTime();
            return datetimeInterval;
        }
    }
}

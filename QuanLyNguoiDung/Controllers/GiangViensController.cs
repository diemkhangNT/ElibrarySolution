using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Dto;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangViensController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly ICrudGVService _crudService;
        private readonly IMapper _mapper;
        private readonly IRefreshToken _refreshToken;

        public GiangViensController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper, ICrudGVService crudService, IRefreshToken refreshToken)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
            _crudService = crudService;
            _refreshToken = refreshToken;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDto login)
        {
            var user = _context.GiangViens.SingleOrDefault(p => p.Username == login.Username
            && p.Password == login.Password);
            if (user == null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Message = "Invalid username/password"
                });
            }
            //Cấp token
            var token = await _crudService.GenarateJwtToken(user);
            return Ok(new AuthResult()
            {
                Result = true,
                Message = "Valid token",
                data = token
            }) ;
        }

        [Route("RenewToken")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            bool refresh = _refreshToken.CheckRefreshToken(tokenModel);
            TokenModel token = new TokenModel();
            if (refresh)
            {
                var storedToken = _context.refreshTokens.FirstOrDefault(x => x.Token == tokenModel.RefreshToken);
                //create new token
                var userGV = _context.GiangViens.SingleOrDefault(gv => gv.MaGV == storedToken.UserID);
                token = await _crudService.GenarateJwtToken(userGV);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Message = "RefeshToken successful!",
                    data = token
                });
            }
            return BadRequest(new AuthResult()
            {
                Result = false,
                Message = "RefeshToken went wrong!",
                data = token
            });
        }

        // GET: api/GiangViens
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangViens()
        {
            if (_context.GiangViens == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_GiangViens();
            return listLTB.ToList();
        }

        // GET: api/GiangViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetGiangVien(string id)
        {
            GiangVien giangVien = await _crudService.Get_GiangVien(id);
            if (giangVien == null)
            {
                return NotFound("Không tìm thấy thông báo có id = " + id + "!");
            }
            else return giangVien;
        }

        // PUT: api/GiangViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(string id, [FromForm] GiangVienDto giangVienDto, IFormFile? HinhDaiDien)
        {
            GiangVien giangVien = _mapper.Map<GiangVien>(giangVienDto);
            if (!_extensionServices.GiangVienExists(id))
            {
                return BadRequest("Không tồn tại mã giảng viên này!");
            }
            if (_extensionServices.IsEmailGVUnique(giangVien.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }
            else if (_extensionServices.IsUserNameGVUnique(giangVien.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }
            else if (!_extensionServices.IsNumberPhone(giangVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            else if (!_extensionServices.ValidatePassword(giangVien.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageGV(giangVien, HinhDaiDien);
            }
            try
            {
                giangVien.MaGV = id;
                await _crudService.Put_GiangVien(giangVien);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extensionServices.GiangVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Thông tin đã được cập nhật!");
        }

        // POST: api/GiangViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiangVien>> PostGiangVien([FromForm] GiangVienDto giangVienDto, IFormFile? HinhDaiDien)
        {
            GiangVien giangVien = _mapper.Map<GiangVien>(giangVienDto);
          if (_context.GiangViens == null)
          {
              return Problem("Entity set 'UserDBContext.GiangViens'  is null.");
          }
          if(_extensionServices.IsEmailGVUnique(giangVien.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }else if (_extensionServices.IsUserNameGVUnique(giangVien.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }else if (!_extensionServices.IsNumberPhone(giangVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }else if (!_extensionServices.ValidatePassword(giangVien.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if(HinhDaiDien != null)
            {
               _extensionServices.UploadImageGV(giangVien, HinhDaiDien);
            }
            try
            {
                _extensionServices.AutoPK_GiangVien(giangVien);
                await _crudService.Post_GiangVien(giangVien);
            }
            catch (DbUpdateException)
            {
                if (_extensionServices.GiangVienExists(giangVien.MaGV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGiangVien", new { id = giangVien.MaGV }, giangVien);
        }

        // DELETE: api/GiangViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiangVien(string id)
        {
            bool flag = await _crudService.Delete_GiangVien(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

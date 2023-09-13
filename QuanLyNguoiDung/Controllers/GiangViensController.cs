using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public GiangViensController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper, ICrudGVService crudService)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/GiangViens
        [HttpGet]
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

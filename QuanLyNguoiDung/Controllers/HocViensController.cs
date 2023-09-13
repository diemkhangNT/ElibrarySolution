using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Dto;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocViensController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly ICrudHVService _crudService;
        private readonly IMapper _mapper;

        public HocViensController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper, ICrudHVService crudHVService)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
            _crudService = crudHVService;
        }

        // GET: api/HocViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocVien>>> GetHocViens()
        {
          if (_context.HocViens == null)
          {
              return NotFound();
          }
          var hocViens = await _crudService.Get_HocViens();
            return hocViens.ToList();
        }

        // GET: api/HocViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocVien>> GetHocVien(string id)
        {
            HocVien hocVien = await _crudService.Get_HovVien(id);
            if (hocVien == null)
            {
                return NotFound("Không tìm thấy thông báo có id = " + id + "!");
            }
            else return hocVien;
        }

        // PUT: api/HocViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocVien(string id,[FromForm] HocVienDto hocVienDto, IFormFile? HinhDaiDien)
        {
            HocVien hocVien = _mapper.Map<HocVien>(hocVienDto);
            if (!_extensionServices.HocVienExists(id))
            {
                return BadRequest("Không tồn tại mã học viên này!");
            }
            if (_extensionServices.IsEmailHVUnique(hocVien.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }
            else if (_extensionServices.IsUserNameHVUnique(hocVien.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }
            else if (!_extensionServices.IsNumberPhone(hocVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            else if (!_extensionServices.ValidatePassword(hocVien.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageHV(hocVien, HinhDaiDien);
            }
            try
            {
                hocVien.MaHV = id;
                await _crudService.Put_HocVien(hocVien);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extensionServices.HocVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/HocViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HocVien>> PostHocVien([FromForm] HocVienDto hocVienDto, IFormFile? HinhDaiDien )
        {
            HocVien hocVien = _mapper.Map<HocVien>(hocVienDto);
          if (_context.HocViens == null)
          {
              return Problem("Entity set 'UserDBContext.HocViens'  is null.");
          }
            if (_extensionServices.IsEmailHVUnique(hocVien.Email))
            {
                return BadRequest("Email này đã được sử dụng! Vui lòng nhập email khác!");
            }
            else if (_extensionServices.IsUserNameHVUnique(hocVien.Username))
            {
                return BadRequest("Tên đăng nhập này đã được sử dụng! Vui lòng nhập tên khác!");
            }
            else if (!_extensionServices.IsNumberPhone(hocVien.SDTLienLac))
            {
                return BadRequest("Số điện thoại không hợp lệ!");
            }
            else if (!_extensionServices.ValidatePassword(hocVien.Password))
            {
                return BadRequest("Password phải ít nhất 8 ký tự, ít nhất một ký tự in hoa, chữ thường, số và ký tự đặt biệt!!");
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageHV(hocVien, HinhDaiDien);
            }
            try
            {
                _extensionServices.AutoPK_HocVien(hocVien);
                await _crudService.Post_HocVien(hocVien);
            }
            catch (DbUpdateException)
            {
                if (_extensionServices.HocVienExists(hocVien.MaHV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetHocVien", new { id = hocVien.MaHV }, hocVien);
        }

        // DELETE: api/HocViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocVien(string id)
        {
            bool flag = await _crudService.Delete_HocVien(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

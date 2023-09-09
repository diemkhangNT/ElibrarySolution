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
using QuanLyNguoiDung.Model;
using QuanLyNguoiDung.Services;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocViensController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly IMapper _mapper;

        public HocViensController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
        }

        // GET: api/HocViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocVien>>> GetHocViens()
        {
          if (_context.HocViens == null)
          {
              return NotFound();
          }
            return await _context.HocViens.ToListAsync();
        }

        // GET: api/HocViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocVien>> GetHocVien(string id)
        {
          if (_context.HocViens == null)
          {
              return NotFound();
          }
            var hocVien = await _context.HocViens.FindAsync(id);

            if (hocVien == null)
            {
                return NotFound();
            }

            return hocVien;
        }

        // PUT: api/HocViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocVien(string id,[FromForm] HocVien hocVien, IFormFile? HinhDaiDien)
        {
            if (id != hocVien.MaHV)
            {
                return BadRequest();
            }
            if (HinhDaiDien != null)
            {
                _extensionServices.UploadImageHV(hocVien, HinhDaiDien);
            }
            _context.Entry(hocVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocVienExists(id))
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
            else
            {
                if (HinhDaiDien != null)
                {
                    _extensionServices.UploadImageHV(hocVien, HinhDaiDien);
                }
                _extensionServices.AutoPK_HocVien(hocVien);
                _context.HocViens.Add(hocVien);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HocVienExists(hocVien.MaHV))
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
            if (_context.HocViens == null)
            {
                return NotFound();
            }
            var hocVien = await _context.HocViens.FindAsync(id);
            if (hocVien == null)
            {
                return NotFound();
            }

            _context.HocViens.Remove(hocVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HocVienExists(string id)
        {
            return (_context.HocViens?.Any(e => e.MaHV == id)).GetValueOrDefault();
        }
    }
}

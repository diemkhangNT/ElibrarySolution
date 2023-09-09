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
using QuanLyNguoiDung.Model;
using QuanLyNguoiDung.Services;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangViensController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly IMapper _mapper;

        public GiangViensController(UserDBContext context, IExtensionServices extensionServices, IMapper mapper)
        {
            _context = context;
            _extensionServices = extensionServices;
            _mapper = mapper;
        }

        // GET: api/GiangViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetGiangViens()
        {
          if (_context.GiangViens == null)
          {
              return NotFound();
          }
            return await _context.GiangViens.ToListAsync();
        }

        // GET: api/GiangViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetGiangVien(string id)
        {
          if (_context.GiangViens == null)
          {
              return NotFound();
          }
            var giangVien = await _context.GiangViens.FindAsync(id);

            if (giangVien == null)
            {
                return NotFound();
            }

            return giangVien;
        }

        // PUT: api/GiangViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(string id, [FromForm] GiangVienDto giangVienDto, IFormFile? HinhDaiDien)
        {
            GiangVien giangVien = _mapper.Map<GiangVien>(giangVienDto);
            if (id != giangVien.MaGV)
            {
                return BadRequest();
            }
            if(HinhDaiDien != null)
            {
                _extensionServices.UploadImageGV(giangVien, HinhDaiDien);
            }
            _context.Entry(giangVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiangVienExists(id))
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
            else
            {
                if(HinhDaiDien != null)
                {
                    _extensionServices.UploadImageGV(giangVien, HinhDaiDien);
                }
                _extensionServices.AutoPK_GiangVien(giangVien);
                _context.GiangViens.Add(giangVien);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GiangVienExists(giangVien.MaGV))
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
            if (_context.GiangViens == null)
            {
                return NotFound();
            }
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return NotFound();
            }

            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiangVienExists(string id)
        {
            return (_context.GiangViens?.Any(e => e.MaGV == id)).GetValueOrDefault();
        }
    }
}

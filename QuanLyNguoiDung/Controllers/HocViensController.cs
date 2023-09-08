using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
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

        public HocViensController(UserDBContext context, IExtensionServices extensionServices)
        {
            _context = context;
            _extensionServices = extensionServices;
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
        public async Task<IActionResult> PutHocVien(string id,[FromForm] HocVien hocVien)
        {
            if (id != hocVien.MaHV)
            {
                return BadRequest();
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
        public async Task<ActionResult<HocVien>> PostHocVien([FromForm] HocVien hocVien)
        {
          if (_context.HocViens == null)
          {
              return Problem("Entity set 'UserDBContext.HocViens'  is null.");
          }
            _extensionServices.AutoPK_HocVien(hocVien);
            _context.HocViens.Add(hocVien);
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

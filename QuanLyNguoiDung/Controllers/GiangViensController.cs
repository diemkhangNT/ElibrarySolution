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
    public class GiangViensController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;

        public GiangViensController(UserDBContext context, IExtensionServices extensionServices)
        {
            _context = context;
            _extensionServices = extensionServices;
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
        public async Task<IActionResult> PutGiangVien(string id,[FromForm] GiangVien giangVien)
        {
            if (id != giangVien.MaGV)
            {
                return BadRequest();
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
        public async Task<ActionResult<GiangVien>> PostGiangVien([FromForm] GiangVien giangVien)
        {
          if (_context.GiangViens == null)
          {
              return Problem("Entity set 'UserDBContext.GiangViens'  is null.");
          }
            _extensionServices.AutoPK_GiangVien(giangVien);
            _context.GiangViens.Add(giangVien);
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

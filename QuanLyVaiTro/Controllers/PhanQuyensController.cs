using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyensController : ControllerBase
    {
        private readonly VaiTroDbContext _context;

        public PhanQuyensController(VaiTroDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanQuyens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanQuyen>>> GetPhanQuyens()
        {
          if (_context.PhanQuyens == null)
          {
              return NotFound();
          }
            return await _context.PhanQuyens.ToListAsync();
        }

        // GET: api/PhanQuyens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanQuyen>> GetPhanQuyen(string id)
        {
          if (_context.PhanQuyens == null)
          {
              return NotFound();
          }
            var phanQuyen = await _context.PhanQuyens.FindAsync(id);

            if (phanQuyen == null)
            {
                return NotFound();
            }

            return phanQuyen;
        }

        // PUT: api/PhanQuyens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanQuyen(string id, PhanQuyen phanQuyen)
        {
            if (id != phanQuyen.MaPQ)
            {
                return BadRequest();
            }

            _context.Entry(phanQuyen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanQuyenExists(id))
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

        // POST: api/PhanQuyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanQuyen>> PostPhanQuyen(PhanQuyen phanQuyen)
        {
          if (_context.PhanQuyens == null)
          {
              return Problem("Entity set 'VaiTroDbContext.PhanQuyens'  is null.");
          }
            _context.PhanQuyens.Add(phanQuyen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhanQuyenExists(phanQuyen.MaPQ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhanQuyen", new { id = phanQuyen.MaPQ }, phanQuyen);
        }

        // DELETE: api/PhanQuyens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanQuyen(string id)
        {
            if (_context.PhanQuyens == null)
            {
                return NotFound();
            }
            var phanQuyen = await _context.PhanQuyens.FindAsync(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            _context.PhanQuyens.Remove(phanQuyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanQuyenExists(string id)
        {
            return (_context.PhanQuyens?.Any(e => e.MaPQ == id)).GetValueOrDefault();
        }
    }
}

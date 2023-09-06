using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NienKhoasController : ControllerBase
    {
        private readonly MonHocDbContext _context;

        public NienKhoasController(MonHocDbContext context)
        {
            _context = context;
        }

        // GET: api/NienKhoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NienKhoa>>> GetNienKhoa()
        {
          if (_context.NienKhoa == null)
          {
              return NotFound();
          }
            return await _context.NienKhoa.ToListAsync();
        }

        // GET: api/NienKhoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NienKhoa>> GetNienKhoa(string id)
        {
          if (_context.NienKhoa == null)
          {
              return NotFound();
          }
            var nienKhoa = await _context.NienKhoa.FindAsync(id);

            if (nienKhoa == null)
            {
                return NotFound();
            }

            return nienKhoa;
        }

        // PUT: api/NienKhoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNienKhoa(string id, NienKhoa nienKhoa)
        {
            if (id != nienKhoa.MaNK)
            {
                return BadRequest();
            }

            _context.Entry(nienKhoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NienKhoaExists(id))
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

        // POST: api/NienKhoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NienKhoa>> PostNienKhoa(NienKhoa nienKhoa)
        {
          if (_context.NienKhoa == null)
          {
              return Problem("Entity set 'MonHocDbContext.NienKhoa'  is null.");
          }
            _context.NienKhoa.Add(nienKhoa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NienKhoaExists(nienKhoa.MaNK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNienKhoa", new { id = nienKhoa.MaNK }, nienKhoa);
        }

        // DELETE: api/NienKhoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNienKhoa(string id)
        {
            if (_context.NienKhoa == null)
            {
                return NotFound();
            }
            var nienKhoa = await _context.NienKhoa.FindAsync(id);
            if (nienKhoa == null)
            {
                return NotFound();
            }

            _context.NienKhoa.Remove(nienKhoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NienKhoaExists(string id)
        {
            return (_context.NienKhoa?.Any(e => e.MaNK == id)).GetValueOrDefault();
        }
    }
}

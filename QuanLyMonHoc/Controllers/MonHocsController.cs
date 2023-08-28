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
    public class MonHocsController : ControllerBase
    {
        private readonly MonHocDbContext _context;

        public MonHocsController(MonHocDbContext context)
        {
            _context = context;
        }

        // GET: api/MonHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonHoc>>> GetMonHocs()
        {
          if (_context.MonHocs == null)
          {
              return NotFound();
          }
            return await _context.MonHocs.ToListAsync();
        }

        // GET: api/MonHocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonHoc>> GetMonHoc(string id)
        {
          if (_context.MonHocs == null)
          {
              return NotFound();
          }
            var monHoc = await _context.MonHocs.FindAsync(id);

            if (monHoc == null)
            {
                return NotFound();
            }

            return monHoc;
        }

        // PUT: api/MonHocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonHoc(string id, MonHoc monHoc)
        {
            if (id != monHoc.MaMH)
            {
                return BadRequest();
            }

            _context.Entry(monHoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonHocExists(id))
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

        // POST: api/MonHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonHoc>> PostMonHoc(MonHoc monHoc)
        {
          if (_context.MonHocs == null)
          {
              return Problem("Entity set 'MonHocDbContext.MonHocs'  is null.");
          }
            _context.MonHocs.Add(monHoc);
            try
            {
               _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MonHocExists(monHoc.MaMH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonHoc", new { id = monHoc.MaMH }, monHoc);
        }

        // DELETE: api/MonHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonHoc(string id)
        {
            if (_context.MonHocs == null)
            {
                return NotFound();
            }
            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc == null)
            {
                return NotFound();
            }

            _context.MonHocs.Remove(monHoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonHocExists(string id)
        {
            return (_context.MonHocs?.Any(e => e.MaMH == id)).GetValueOrDefault();
        }
    }
}

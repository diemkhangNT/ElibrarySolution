using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Model;
using QuanLyMonHoc.Services;

namespace QuanLyMonHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NienKhoasController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;

        public NienKhoasController(MonHocDbContext context, IExtension extension)
        {
            _context = context;
            _extension = extension;
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
        public async Task<IActionResult> PutNienKhoa(string id, [FromForm] NienKhoa nienKhoa)
        {
            if (id != nienKhoa.MaNK)
            {
                return BadRequest();
            }
            if(_extension.IsCheckTime_put(nienKhoa.TGBatDau, nienKhoa.TGKetThuc, nienKhoa.MaNK))
            {
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
            }
            else
            {
                return BadRequest("Thời gian không hợp lệ! ");
            }
            return NoContent();
        }

        // POST: api/NienKhoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NienKhoa>> PostNienKhoa([FromForm] NienKhoa nienKhoa)
        {
          if (_context.NienKhoa == null)
          {
              return Problem("Entity set 'MonHocDbContext.NienKhoa'  is null.");
          }
          if(_extension.IsCheckTime(nienKhoa.TGBatDau, nienKhoa.TGKetThuc))
            {
                _extension.AutoPK_NienKhoa(nienKhoa);
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
            }
          else return BadRequest("Thời gian không hợp lệ! ");


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

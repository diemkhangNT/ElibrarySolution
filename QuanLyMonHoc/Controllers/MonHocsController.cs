using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Dto;
using QuanLyMonHoc.Model;
using QuanLyMonHoc.Services;

namespace QuanLyMonHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly IMapper _mapper;
        public MonHocsController(MonHocDbContext context, IExtension extension, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
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
        public async Task<IActionResult> PutMonHoc(string id, [FromForm] MonHocDto monHocDto)
        {
            MonHoc monHoc = _mapper.Map<MonHoc>(monHocDto);
            if (id != monHoc.MaMH)
            {
                return BadRequest();
            }
            if (_extension.IsExistNameMonHoc_Put(monHoc))
            {
                monHoc.NgayGuiPheDuyet = DateTime.Now;  
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
            }
            else return BadRequest("Tên môn học này đã tồn tại!");
            return NoContent();
        }

        // POST: api/MonHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonHoc>> PostMonHoc([FromForm] MonHocDto monHocdto)
        {
            MonHoc monHoc = _mapper.Map<MonHoc>(monHocdto);
          if (_context.MonHocs == null)
          {
              return Problem("Entity set 'MonHocDbContext.MonHocs'  is null.");
          }
            else
            {
                if (_extension.IsExistNameMonHoc_Post(monHoc.TenMH)){
                    return BadRequest("Tên môn học này đã tồn tại!");
                }
                else
                {
                    _extension.AutoPK_MonHoc(monHoc);
                    _context.MonHocs.Add(monHoc);
                    try
                    {
                        await _context.SaveChangesAsync();
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

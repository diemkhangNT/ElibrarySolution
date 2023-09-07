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
    public class LopHocsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly IMapper _mapper;

        public LopHocsController(MonHocDbContext context, IExtension extension, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
        }

        // GET: api/LopHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LopHoc>>> GetLopHoc()
        {
          if (_context.LopHoc == null)
          {
              return NotFound();
          }
            return await _context.LopHoc.ToListAsync();
        }

        // GET: api/LopHocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LopHoc>> GetLopHoc(string id)
        {
          if (_context.LopHoc == null)
          {
              return NotFound();
          }
            var lopHoc = await _context.LopHoc.FindAsync(id);

            if (lopHoc == null)
            {
                return NotFound();
            }

            return lopHoc;
        }

        // PUT: api/LopHocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLopHoc(string id, [FromForm] LopHocDto lopHocDto)
        {
            LopHoc lopHoc = _mapper.Map<LopHoc>(lopHocDto);
            if (id != lopHoc.MaLop)
            {
                return BadRequest();
            }

            _context.Entry(lopHoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LopHocExists(id))
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

        // POST: api/LopHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LopHoc>> PostLopHoc([FromForm] LopHocDto lopHocDto)
        {
            LopHoc lopHoc = _mapper.Map<LopHoc>(lopHocDto);
          if (_context.LopHoc == null)
          {
              return Problem("Entity set 'MonHocDbContext.LopHoc'  is null.");
          }
            _extension.AutoPK_LopHoc(lopHoc);
            _context.LopHoc.Add(lopHoc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LopHocExists(lopHoc.MaLop))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLopHoc", new { id = lopHoc.MaLop }, lopHoc);
        }

        // DELETE: api/LopHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLopHoc(string id)
        {
            if (_context.LopHoc == null)
            {
                return NotFound();
            }
            var lopHoc = await _context.LopHoc.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            _context.LopHoc.Remove(lopHoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LopHocExists(string id)
        {
            return (_context.LopHoc?.Any(e => e.MaLop == id)).GetValueOrDefault();
        }
    }
}

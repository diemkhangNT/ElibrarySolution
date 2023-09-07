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
    public class TraLoisController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly IMapper _mapper;

        public TraLoisController(MonHocDbContext context, IExtension extension, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
        }

        // GET: api/TraLois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraLoi>>> GetTraLoi()
        {
          if (_context.TraLoi == null)
          {
              return NotFound();
          }
            return await _context.TraLoi.ToListAsync();
        }

        // GET: api/TraLois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraLoi>> GetTraLoi(string id)
        {
          if (_context.TraLoi == null)
          {
              return NotFound();
          }
            var traLoi = await _context.TraLoi.FindAsync(id);

            if (traLoi == null)
            {
                return NotFound();
            }

            return traLoi;
        }

        // PUT: api/TraLois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraLoi(string id, [FromForm] TraLoiDto traLoiDto)
        {
            TraLoi traLoi = _mapper.Map<TraLoi>(traLoiDto);
            if (id != traLoi.MaCauTL)
            {
                return BadRequest();
            }
            traLoi.ThoiGian = DateTime.Now;
            _context.Entry(traLoi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraLoiExists(id))
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

        // POST: api/TraLois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraLoi>> PostTraLoi([FromForm] TraLoiDto traLoiDto)
        {
            TraLoi traLoi = _mapper.Map<TraLoi>(traLoiDto);
            if (_context.TraLoi == null)
          {
              return Problem("Entity set 'MonHocDbContext.TraLoi'  is null.");
          }
            _extension.AutoPK_TraLoi(traLoi);
            _context.TraLoi.Add(traLoi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TraLoiExists(traLoi.MaCauTL))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraLoi", new { id = traLoi.MaCauTL }, traLoi);
        }

        // DELETE: api/TraLois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraLoi(string id)
        {
            if (_context.TraLoi == null)
            {
                return NotFound();
            }
            var traLoi = await _context.TraLoi.FindAsync(id);
            if (traLoi == null)
            {
                return NotFound();
            }

            _context.TraLoi.Remove(traLoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraLoiExists(string id)
        {
            return (_context.TraLoi?.Any(e => e.MaCauTL == id)).GetValueOrDefault();
        }
    }
}

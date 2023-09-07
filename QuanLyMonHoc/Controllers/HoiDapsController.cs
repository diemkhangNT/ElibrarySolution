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
    public class HoiDapsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly IMapper _mapper;


        public HoiDapsController(MonHocDbContext context, IExtension extension, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
        }

        // GET: api/HoiDaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoiDap>>> GetHoiDap()
        {
          if (_context.HoiDap == null)
          {
              return NotFound();
          }
            return await _context.HoiDap.ToListAsync();
        }

        // GET: api/HoiDaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoiDap>> GetHoiDap(string id)
        {
          if (_context.HoiDap == null)
          {
              return NotFound();
          }
            var hoiDap = await _context.HoiDap.FindAsync(id);

            if (hoiDap == null)
            {
                return NotFound();
            }

            return hoiDap;
        }

        // PUT: api/HoiDaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoiDap(string id, [FromForm] HoiDapDto hoiDapDto)
        {
            HoiDap hoiDap = _mapper.Map<HoiDap>(hoiDapDto);
            if (id != hoiDap.MaCauHoi)
            {
                return BadRequest();
            }
            hoiDap.ThoiGian = DateTime.Now;
            _context.Entry(hoiDap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoiDapExists(id))
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

        // POST: api/HoiDaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HoiDap>> PostHoiDap([FromForm] HoiDapDto hoiDapDto)
        {
            HoiDap hoiDap = _mapper.Map<HoiDap>(hoiDapDto);
            if (_context.HoiDap == null)
          {
              return Problem("Entity set 'MonHocDbContext.HoiDap'  is null.");
          }
            _extension.AutoPK_HoiDap(hoiDap);
            _context.HoiDap.Add(hoiDap);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HoiDapExists(hoiDap.MaCauHoi))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest("Số ký tự vượt quá số lượng cho phép! (Tiêu đề < 100 ký tự & Nội dung < 500 ký tự");
                }
            }

            return CreatedAtAction("GetHoiDap", new { id = hoiDap.MaCauHoi }, hoiDap);
        }

        // DELETE: api/HoiDaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoiDap(string id)
        {
            if (_context.HoiDap == null)
            {
                return NotFound();
            }
            var hoiDap = await _context.HoiDap.FindAsync(id);
            if (hoiDap == null)
            {
                return NotFound();
            }

            _context.HoiDap.Remove(hoiDap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoiDapExists(string id)
        {
            return (_context.HoiDap?.Any(e => e.MaCauHoi == id)).GetValueOrDefault();
        }
    }
}

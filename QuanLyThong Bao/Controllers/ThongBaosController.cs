using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Dto;
using QuanLyThong_Bao.Model;
using QuanLyThong_Bao.Services;

namespace QuanLyThong_Bao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaosController : ControllerBase
    {
        private readonly ThongBaoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExtentionSV _extentionSV;

        public ThongBaosController(ThongBaoDbContext context, IExtentionSV extentionSV, IMapper mapper)
        {
            _context = context;
            _extentionSV = extentionSV;
            _mapper = mapper;
        }

        // GET: api/ThongBaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThongBao>>> GetthongBaos()
        {
          if (_context.thongBaos == null)
          {
              return NotFound();
          }
            return await _context.thongBaos.ToListAsync();
        }

        // GET: api/ThongBaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThongBao>> GetThongBao(string id)
        {
          if (_context.thongBaos == null)
          {
              return NotFound();
          }
            var thongBao = await _context.thongBaos.FindAsync(id);

            if (thongBao == null)
            {
                return NotFound();
            }

            return thongBao;
        }

        // PUT: api/ThongBaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThongBao(string id, ThongBao thongBao)
        {
            if (id != thongBao.MaTB)
            {
                return BadRequest();
            }

            _context.Entry(thongBao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongBaoExists(id))
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

        // POST: api/ThongBaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ThongBao>> PostThongBao([FromForm] ThongBaoDto thongBaoDto)
        {
            ThongBao thongBao = _mapper.Map<ThongBao>(thongBaoDto);
          if (_context.thongBaos == null)
          {
              return Problem("Entity set 'ThongBaoDbContext.thongBaos'  is null.");
          }
            _extentionSV.AutoPK_ThongBao(thongBao);
            _context.thongBaos.Add(thongBao);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ThongBaoExists(thongBao.MaTB))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetThongBao", new { id = thongBao.MaTB }, thongBao);
        }

        // DELETE: api/ThongBaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThongBao(string id)
        {
            if (_context.thongBaos == null)
            {
                return NotFound();
            }
            var thongBao = await _context.thongBaos.FindAsync(id);
            if (thongBao == null)
            {
                return NotFound();
            }

            _context.thongBaos.Remove(thongBao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThongBaoExists(string id)
        {
            return (_context.thongBaos?.Any(e => e.MaTB == id)).GetValueOrDefault();
        }
    }
}

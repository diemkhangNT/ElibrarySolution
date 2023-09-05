using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Dto;
using QuanLyVaiTro.Model;
using QuanLyVaiTro.Services;

namespace QuanLyVaiTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroesController : ControllerBase
    {
        private readonly VaiTroDbContext _context;
        private readonly ICrudService _icrudService;
        private readonly IMapper _mapper;

        public VaiTroesController(VaiTroDbContext context, IMapper mapper, ICrudService icrudService)
        {
            _context = context;
            _mapper = mapper;
            _icrudService = icrudService;
        }

        // GET: api/VaiTroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaiTro>>> GetVaiTros()
        {
          List<VaiTro> vaiTros = (List<VaiTro>)await _icrudService.Get_VaiTros();
          if (vaiTros == null)
          {
              return NotFound("Không tìm thấy dữ liệu!");
          }
          return Ok(vaiTros.Select(vt => _mapper.Map<VaiTroDto>(vt)));
        }

        // GET: api/VaiTroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VaiTro>> GetVaiTro(string id)
        {
            VaiTro vt = await _icrudService.Get_VaiTro(id);
            if (vt == null)
            {
                return NotFound("Không tìm thấy dữ liệu có mã " + id + "!!!");
            }
            return Ok(_mapper.Map<VaiTroDto>(vt));
        }

        // PUT: api/VaiTroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaiTro(string id, VaiTroDto vaiTroVM)
        {
            if (id != vaiTroVM.MaVT)
            {
                return BadRequest("Không tìm thấy dữ liệu!"); 
            }
            VaiTro vt = await _icrudService.Get_VaiTro(id);
            _icrudService.Put_VaiTro(vt);
            _mapper.Map(vaiTroVM, vt);
            _context.Entry(vt).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_icrudService.VaiTroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVaiTro", new { id = vaiTroVM.MaVT }, vaiTroVM); 
        }

        // POST: api/VaiTroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VaiTro>> PostVaiTro(VaiTroDto vaiTroVM)
        {
            var vt = _mapper.Map<VaiTro>(vaiTroVM);
            _icrudService.Post_VaiTro(vt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_icrudService.VaiTroExists(vt.MaVT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetVaiTro", new { id = vaiTroVM.MaVT }, vaiTroVM);
        }

        // DELETE: api/VaiTroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaiTro(string id)
        {
            var vaiTro = await _icrudService.Get_VaiTro(id);
            if (vaiTro == null)
            {
                return NotFound("Không tìm thấy dữ liệu");
            }
            _context.VaiTros.Remove(vaiTro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}

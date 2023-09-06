using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyVaiTro.Data;
using QuanLyVaiTro.Dto;
using QuanLyVaiTro.Model;
using QuanLyVaiTro.Services;

namespace QuanLyVaiTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyensController : ControllerBase
    {
        private readonly VaiTroDbContext _context;
        private readonly ICrudService _icrudService;
        private readonly IMapper _mapper;
        public PhanQuyensController(VaiTroDbContext context, ICrudService icrudService, IMapper mapper)
        {
            _context = context;
            _icrudService = icrudService;
            _mapper = mapper;
        }

        // GET: api/PhanQuyens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanQuyen>>> GetPhanQuyens()
        {
            List<PhanQuyen> phanQuyens = (List<PhanQuyen>)await _icrudService.Get_PhanQuyens();
            if (phanQuyens == null)
            {
                return NotFound("Không tìm thấy dữ liệu!");
            }
            return Ok(phanQuyens.Select(pq => _mapper.Map<PhanQuyenDto>(pq)));
        }

        // GET: api/PhanQuyens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanQuyen>> GetPhanQuyen(string id)
        {
            PhanQuyen pq = await _icrudService.Get_PhanQuyen(id);
            if (pq == null)
            {
                return NotFound("Không tìm thấy dữ liệu có mã " + id + "!!!");
            }
            return Ok(_mapper.Map<PhanQuyenDto>(pq));
        }

        // PUT: api/PhanQuyens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanQuyen(string id, [FromForm] PhanQuyenDto phanQuyenVM)
        {
            if (id != phanQuyenVM.MaPQ)
            {
                return BadRequest("Không tìm thấy dữ liệu!");
            }
            PhanQuyen pq = await _icrudService.Get_PhanQuyen(id);
            _mapper.Map(phanQuyenVM, pq);
            _context.Entry(pq).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_icrudService.PhanQuyenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhanQuyen", new { id = pq.MaPQ }, pq);
        }

        // POST: api/PhanQuyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanQuyen>> PostPhanQuyen([FromForm]PhanQuyenDto phanQuyenVM)
        {
            var pq = _mapper.Map<PhanQuyen>(phanQuyenVM);
            _icrudService.Post_PhanQuyen(pq);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_icrudService.PhanQuyenExists(pq.MaPQ))
                {
                    return Conflict("Khóa chính đã tồn tại!!");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPhanQuyen", new { id = pq.MaPQ }, pq);
        }

        // DELETE: api/PhanQuyens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanQuyen(string id)
        {
            var phanQuyen = await _icrudService.Get_PhanQuyen(id);
            if (phanQuyen == null)
            {
                return NotFound("Không tìm thấy dữ liệu!");
            }

            _context.PhanQuyens.Remove(phanQuyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}

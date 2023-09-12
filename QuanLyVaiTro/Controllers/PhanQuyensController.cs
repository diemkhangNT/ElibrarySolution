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
using QuanLyVaiTro.Interface;
using QuanLyVaiTro.Model;
using QuanLyVaiTro.Services;

namespace QuanLyVaiTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyensController : ControllerBase
    {
        private readonly VaiTroDbContext _context;
        private readonly ICrudPhanQuyenService _icrudService;
        private readonly IMapper _mapper;
        public PhanQuyensController(VaiTroDbContext context, ICrudPhanQuyenService icrudService, IMapper mapper)
        {
            _context = context;
            _icrudService = icrudService;
            _mapper = mapper;
        }

        // GET: api/PhanQuyens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanQuyen>>> GetPhanQuyens()
        {
            if (_context.PhanQuyens == null)
            {
                return NotFound("Không tìm thấy dữ liệu!");
            }
            var phanQuyens = await _icrudService.Get_PhanQuyens();
            return Ok(phanQuyens.ToList());
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
            PhanQuyen phanQuyen = _mapper.Map<PhanQuyen>(phanQuyenVM);
            if (!_icrudService.PhanQuyenExists(id))
            {
                return BadRequest($"Không tồn tại mã {id} trong database!");
            }
            else
            {
                phanQuyen.MaPQ = id;
                await _icrudService.Put_PhanQuyen(phanQuyen);
            }
            return Ok("Nội dung đã được cập nhật!");
        }

        // POST: api/PhanQuyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanQuyen>> PostPhanQuyen([FromForm]PhanQuyenDto phanQuyenVM)
        {
            var pq = _mapper.Map<PhanQuyen>(phanQuyenVM);
            if (_context.PhanQuyens == null)
            {
                return Problem("Entity set 'ThongBaoDbContext.thongBaos'  is null.");
            }
            try
            {
                await _icrudService.Post_PhanQuyen(pq);
            }
            catch (DbUpdateException)
            {
                if (_icrudService.PhanQuyenExists(pq.MaPQ))
                {
                    return Conflict();
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
            bool flag = await _icrudService.Delete_PhanQuyen(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
        
    }
}

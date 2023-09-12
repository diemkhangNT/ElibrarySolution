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
using QuanLyVaiTro.Interface;
using QuanLyVaiTro.Model;

namespace QuanLyVaiTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroesController : ControllerBase
    {
        private readonly VaiTroDbContext _context;
        private readonly ICrudVaiTroService _icrudService;
        private readonly IMapper _mapper;

        public VaiTroesController(VaiTroDbContext context, IMapper mapper, ICrudVaiTroService icrudService)
        {
            _context = context;
            _mapper = mapper;
            _icrudService = icrudService;
        }

        // GET: api/VaiTroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaiTro>>> GetVaiTros()
        {
          if (_context.VaiTros == null)
          {
              return NotFound("Không tìm thấy dữ liệu!");
          }
            List<VaiTro> vaiTros = (List<VaiTro>)await _icrudService.Get_VaiTros();
            return Ok(vaiTros);
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
        public async Task<IActionResult> PutVaiTro(string id,[FromForm] VaiTroDto vaiTroVM)
        {
            VaiTro vaiTro = _mapper.Map<VaiTro>(vaiTroVM);
            if (!_icrudService.VaiTroExists(id))
            {
                return BadRequest($"Không tồn tại mã {id} trong database!");
            }
            else
            {
                vaiTro.MaVT = id;
                await _icrudService.Put_VaiTro(vaiTro);
            }
            return Ok("Nội dung đã được cập nhật!");
        }

        // POST: api/VaiTroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VaiTro>> PostVaiTro([FromForm] VaiTroDto vaiTroVM)
        {
            VaiTro vaiTro = _mapper.Map<VaiTro>(vaiTroVM);
            if (_context.VaiTros == null)
            {
                return Problem("Entity set 'ThongBaoDbContext.thongBaos'  is null.");
            }
            try
            {
                await _icrudService.Post_VaiTro(vaiTro);
            }
            catch (DbUpdateException)
            {
                if (_icrudService.VaiTroExists(vaiTro.MaVT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetVaiTro", new { id = vaiTro.MaVT }, vaiTro);
        }

        // DELETE: api/VaiTroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaiTro(string id)
        {
            bool flag = await _icrudService.Delete_VaiTro(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

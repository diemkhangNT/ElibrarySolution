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
using QuanLyThong_Bao.Interfaces;
using QuanLyThong_Bao.Model;
using QuanLyThong_Bao.Services;

namespace QuanLyThong_Bao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiThongBaosController : ControllerBase
    {
        private readonly ThongBaoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExtentionService _extention;
        private readonly ICrudTypeAnnounce _crudService;

        public LoaiThongBaosController(ThongBaoDbContext context, IMapper mapper, IExtentionService extention, ICrudTypeAnnounce crudService)
        {
            _context = context;
            _mapper = mapper;
            _extention = extention;
            _crudService = crudService;
        }

        // GET: api/LoaiThongBaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiThongBao>>> GetloaiThongBaos()
        {
              if (_context.loaiThongBaos == null)
              {
                  return NotFound();
              }
              var listLTB = await _crudService.Get_LoaiThongBaos();
              return listLTB.ToList();
        }

        // GET: api/LoaiThongBaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiThongBao>> GetLoaiThongBao(string id)
        {
            LoaiThongBao loaiTB = await _crudService.Get_LoaiThongBao(id);
            if (loaiTB == null)
            {
                return NotFound("Không tìm thấy loại thông báo có id = " + id + "!");
            }
            else return loaiTB;
        }

        // PUT: api/LoaiThongBaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiThongBao(string id,[FromForm] LoaiTBDto loaiThongBaoDto)
        {
            LoaiThongBao loaiThongBao =  _mapper.Map<LoaiThongBao>(loaiThongBaoDto);
            if (!_extention.LoaiThongBaoExists(id))
            {
                return BadRequest($"Không tồn tại mã {id} trong database!");
            }
            else
            {
                if (_extention.IsExistNameLoaiTB(loaiThongBao.TenLTB)) 
                    return BadRequest("Tên loại thông báo này đã tồn tại!");
                else
                {
                    loaiThongBao.MaLTB = id;
                    await _crudService.Put_LoaiThongBao(loaiThongBao);
                }
            }
            return Ok("Nội dung đã được cập nhật!");
        }

        // POST: api/LoaiThongBaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoaiThongBao>> PostLoaiThongBao([FromForm] LoaiTBDto loaiThongBaoDto)
        {
            LoaiThongBao loaiThongBao = _mapper.Map<LoaiThongBao>(loaiThongBaoDto);
          if (_context.loaiThongBaos == null)
          {
              return Problem("Entity set 'ThongBaoDbContext.loaiThongBaos'  is null.");
          }
            _extention.AutoPK_LoaiTB(loaiThongBao);
            try
            {
                if (_extention.IsExistNameLoaiTB(loaiThongBao.TenLTB))
                    return BadRequest("Tên loại thông báo này đã tồn tại!");
                await _crudService.Post_LoaiThongBao(loaiThongBao);
            }
            catch (DbUpdateException)
            {
                if (_extention.LoaiThongBaoExists(loaiThongBao.MaLTB))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetLoaiThongBao", new { id = loaiThongBao.MaLTB }, loaiThongBao);
        }

        // DELETE: api/LoaiThongBaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiThongBao(string id)
        {
            bool flag = await _crudService.Delete_LoaiThongbao(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

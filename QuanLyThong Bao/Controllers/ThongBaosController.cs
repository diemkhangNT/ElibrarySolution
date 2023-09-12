using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using QuanLyThong_Bao.Data;
using QuanLyThong_Bao.Dto;
using QuanLyThong_Bao.Interfaces;
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
        private readonly IExtentionService _extentionSV;
        private readonly ICrudAnnounce _crudService;

        public ThongBaosController(ThongBaoDbContext context, IExtentionService extentionSV, IMapper mapper, ICrudAnnounce crudService)
        {
            _context = context;
            _extentionSV = extentionSV;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/ThongBaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThongBao>>> GetthongBaos()
        {
            if (_context.thongBaos == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_ThongBaos();
            return listLTB.ToList();
        }

        // GET: api/ThongBaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThongBao>> GetThongBao(string id)
        {
            ThongBao thongBao = await _crudService.Get_ThongBao(id);
            if (thongBao == null)
            {
                return NotFound("Không tìm thấy thông báo có id = " + id + "!");
            }
            else return thongBao;
        }

        // PUT: api/ThongBaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThongBao(string id,[FromForm] ThongBaoDto thongBaoDto)
        {
            ThongBao thongBao = _mapper.Map<ThongBao>(thongBaoDto);
            if (!_extentionSV.ThongBaoExists(id))
            {
                return BadRequest($"Không tồn tại mã {id} trong database!");
            }
            else
            {
                thongBao.MaTB = id;
                await _crudService.Put_ThongBao(thongBao);
            }
            return Ok("Nội dung đã được cập nhật!");
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
            try
            {
                await _crudService.Post_ThongBao(thongBao);
            }
            catch (DbUpdateException)
            {
                if (_extentionSV.ThongBaoExists(thongBao.MaTB))
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
            bool flag = await _crudService.Delete_ThongBao(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }

    }
}

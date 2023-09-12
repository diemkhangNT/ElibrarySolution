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

namespace QuanLyThong_Bao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuiThongBaosController : ControllerBase
    {
        private readonly ThongBaoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExtentionService _extentionsv;
        private readonly ICrudSendAnnounce _crudService;

        public GuiThongBaosController(ThongBaoDbContext context, IMapper mapper, IExtentionService extentionsv, ICrudSendAnnounce crudService)
        {
            _context = context;
            _mapper = mapper;
            _extentionsv = extentionsv;
            _crudService = crudService;
        }

        // GET: api/GuiThongBaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuiThongBao>>> GetguiThongBaos()
        {
            if (_context.guiThongBaos == null)
            {
                return NotFound();
            }
            var listGTB = await _crudService.Get_GuiTBs();
            return listGTB.ToList();
        }

        // GET: api/GuiThongBaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuiThongBao>> GetGuiThongBao(int id)
        {
            GuiThongBao guiThongBao = await _crudService.Get_GuiTB(id);
            if (guiThongBao == null)
            {
                return NotFound("Không tìm thấy id = " + id + "!");
            }
            else return guiThongBao;
        }

        // PUT: api/GuiThongBaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuiThongBao(int id,[FromForm] GuiThongBaoDto guiThongBaoDto)
        {
            GuiThongBao guiThongBao = _mapper.Map<GuiThongBao>(guiThongBaoDto);
            if (!_extentionsv.GuiThongBaoExists(id))
            {
                return BadRequest($"Không tồn tại mã {id} trong database!");
            }
            else
            {
                guiThongBao.STT = id;
                await _crudService.Put_GuiTB(guiThongBao);
            }
            return Ok("Nội dung đã được cập nhật!");
        }

        // POST: api/GuiThongBaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GuiThongBao>> PostGuiThongBao([FromForm] GuiThongBaoDto guiThongBaoDto)
        {
          
            GuiThongBao guiThongBao = _mapper.Map<GuiThongBao>(guiThongBaoDto);
            if (_context.guiThongBaos == null)
            {
                return Problem("Entity set 'ThongBaoDbContext.guiThongBaos'  is null.");
            }
            try
            {
                await _crudService.Post_GuiTB(guiThongBao);
            }
            catch (DbUpdateException)
            {
                if (_extentionsv.GuiThongBaoExists(guiThongBao.STT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetGuiThongBao", new { id = guiThongBao.STT }, guiThongBao);
        }

        // DELETE: api/GuiThongBaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuiThongBao(int id)
        {
            bool flag = await _crudService.Delete_GuiTB(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

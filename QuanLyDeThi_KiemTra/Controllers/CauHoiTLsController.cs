using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Dto;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiTLsController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly ICrudCauHoiTuLuan _crudService;
        private readonly IMapper _mapper;

        public CauHoiTLsController(DeThiDbContext context, ICrudCauHoiTuLuan crudService, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _mapper = mapper;
        }

        // GET: api/CauHoiTLs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CauHoiTL>>> GetCauHoiTLs()
        {
            if (_context.CauHoiTLs == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_CHTuLuans();
            return listLTB.ToList();
        }

        // GET: api/CauHoiTLs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CauHoiTL>> GetCauHoiTL(string id)
        {
            CauHoiTL cH = await _crudService.Get_CHTuLuan(id);
            if (cH == null)
            {
                return NotFound("Không tìm thấy câu hỏi tự luận có id = " + id + "!");
            }
            else return cH;
        }

        // PUT: api/CauHoiTLs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCauHoiTL(string id, [FromForm] CauHoiTuLuanDto cauHoiDto)
        {
            CauHoiTL cH = _mapper.Map<CauHoiTL>(cauHoiDto);
            if (!_crudService.CHTuLuanExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                cH.MaCH = id;
                await _crudService.Put_CHTuLuan(cH);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.CHTuLuanExists(id))
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

        // POST: api/CauHoiTLs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CauHoiTL>> PostCauHoiTL([FromForm] CauHoiTuLuanDto cauHoiDto)
        {
            CauHoiTL cH = _mapper.Map<CauHoiTL>(cauHoiDto);
            if (_context.CauHoiTLs == null)
            {
                return Problem("Entity set 'DeThiDbContext.ChtuLuans'  is null.");
            }
            try
            {
                await _crudService.Post_CHTuLuan(cH);
            }
            catch (DbUpdateException)
            {
                if (_crudService.CHTuLuanExists(cH.MaCH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCauHoiTL", new { id = cH.MaCH }, cH);
        }

        // DELETE: api/CauHoiTLs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCauHoiTL(string id)
        {
            bool flag = await _crudService.Delete_CHTuLuan(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

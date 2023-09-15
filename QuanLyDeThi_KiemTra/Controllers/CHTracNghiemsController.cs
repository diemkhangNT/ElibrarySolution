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
    public class CHTracNghiemsController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly ICrudCauHoiTracNghiem _crudService;
        private readonly IMapper _mapper;

        public CHTracNghiemsController(DeThiDbContext context, ICrudCauHoiTracNghiem crudService, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _mapper = mapper;
        }

        // GET: api/CHTracNghiems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CHTracNghiem>>> GetChTracNghiems()
        {
            if (_context.ChTracNghiems == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_CHTracNghiems();
            return listLTB.ToList();
        }

        // GET: api/CHTracNghiems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CHTracNghiem>> GetCHTracNghiem(string id)
        {
            CHTracNghiem cHTracNghiem = await _crudService.Get_CHTracNghiem(id);
            if (cHTracNghiem == null)
            {
                return NotFound("Không tìm thấy câu hỏi trắc nghiệm có id = " + id + "!");
            }
            else return cHTracNghiem;
        }

        // PUT: api/CHTracNghiems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCHTracNghiem(string id,[FromForm] CauHoiTracNghiemDto cauHoiDto)
        {
            CHTracNghiem cH = _mapper.Map<CHTracNghiem>(cauHoiDto);
            if (!_crudService.CHTracNghiemExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                cH.MaCHTrN = id;
                await _crudService.Put_CHTracNghiem(cH);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.CHTracNghiemExists(id))
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

        // POST: api/CHTracNghiems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CHTracNghiem>> PostCHTracNghiem([FromForm] CauHoiTracNghiemDto cauHoiDto)
        {
            CHTracNghiem cH = _mapper.Map<CHTracNghiem>(cauHoiDto);
            if (_context.ChTracNghiems == null)
          {
              return Problem("Entity set 'DeThiDbContext.ChTracNghiems'  is null.");
          }
            try
            {
                await _crudService.Post_CHTracNghiem(cH);
            }
            catch (DbUpdateException)
            {
                if (_crudService.CHTracNghiemExists(cH.MaCHTrN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCHTracNghiem", new { id = cH.MaCHTrN }, cH);
        }

        // DELETE: api/CHTracNghiems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCHTracNghiem(string id)
        {
            bool flag = await _crudService.Delete_CHTracNghiem(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

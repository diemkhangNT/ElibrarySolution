using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TLTracNghiemsController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly ICrudTraLoiTracNghiem _crudService;
        private readonly IMapper _mapper;

        public TLTracNghiemsController(DeThiDbContext context, ICrudTraLoiTracNghiem crudService, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _mapper = mapper;
        }

        // GET: api/TLTracNghiems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TLTracNghiem>>> GetTLTracNghiems()
        {
          if (_context.TLTracNghiems == null)
          {
              return NotFound();
          }
            var listLTB = await _crudService.Get_TLTracNghiems();
            return listLTB.ToList();
        }

        // GET: api/TLTracNghiems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TLTracNghiem>> GetTLTracNghiem(string id)
        {
            TLTracNghiem tL = await _crudService.Get_TLTracNghiem(id);
            if (tL == null)
            {
                return NotFound("Không tìm thấy câu hỏi trắc nghiệm có id = " + id + "!");
            }
            else return tL;
        }

        // PUT: api/TLTracNghiems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTLTracNghiem(string id, TLTracNghiem tLTracNghiem)
        {
            TLTracNghiem tL = _mapper.Map<TLTracNghiem>(tLTracNghiem);
            if (!_crudService.TLTracNghiemExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                tL.MaCH = id;
                await _crudService.Put_TLTracNghiem(tL);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.TLTracNghiemExists(id))
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

        // POST: api/TLTracNghiems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TLTracNghiem>> PostTLTracNghiem(TLTracNghiem tLTracNghiem)
        {
            if (_context.TLTracNghiems == null)
          {
              return Problem("Entity set 'DeThiDbContext.TLTracNghiems'  is null.");
          }
            try
            {
                await _crudService.Post_TLTracNghiem(tLTracNghiem);
            }
            catch (DbUpdateException)
            {
                if (_crudService.TLTracNghiemExists(tLTracNghiem.MaCH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTLTracNghiem", new { id = tLTracNghiem.MaCH }, tLTracNghiem);
        }

        // DELETE: api/TLTracNghiems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTLTracNghiem(string id)
        {
            bool flag = await _crudService.Delete_TLTracNghiem(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

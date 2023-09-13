using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonHoc.Data;
using QuanLyMonHoc.Dto;
using QuanLyMonHoc.Interface;
using QuanLyMonHoc.Model;

namespace QuanLyMonHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoiDapsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly ICrudCauHoi _crudService;
        private readonly IMapper _mapper;


        public HoiDapsController(MonHocDbContext context, IExtension extension, IMapper mapper, ICrudCauHoi crudService)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/HoiDaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoiDap>>> GetHoiDap()
        {
            if (_context.HoiDaps == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_HoiDaps();
            return listLTB.ToList();
        }

        // GET: api/HoiDaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoiDap>> GetHoiDap(string id)
        {
            HoiDap hoiDap = await _crudService.Get_HoiDap(id);
            if (hoiDap == null)
            {
                return NotFound("Không tìm thấy câu hỏi có id = " + id + "!");
            }
            else return hoiDap;
        }

        // PUT: api/HoiDaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoiDap(string id, [FromForm] HoiDapDto hoiDapDto)
        {
            HoiDap hoiDap = _mapper.Map<HoiDap>(hoiDapDto);
            if (!_extension.HoiDapExists(id))
            {
                return NotFound("Không tìm thấy mã này!");
            }
            try
            {
                hoiDap.MaCauHoi = id;
                await _crudService.Put_HoiDap(hoiDap);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extension.HoiDapExists(id))
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

        // POST: api/HoiDaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HoiDap>> PostHoiDap([FromForm] HoiDapDto hoiDapDto)
        {
            HoiDap hoiDap = _mapper.Map<HoiDap>(hoiDapDto);
            if (_context.HoiDaps == null)
          {
              return Problem("Entity set 'MonHocDbContext.HoiDap'  is null.");
          }
            try
            {
                _extension.AutoPK_HoiDap(hoiDap);
                await _crudService.Post_HoiDap(hoiDap);
            }
            catch (DbUpdateException)
            {
                if (_extension.HoiDapExists(hoiDap.MaCauHoi))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest("Số ký tự vượt quá số lượng cho phép! (Tiêu đề < 100 ký tự & Nội dung < 500 ký tự");
                }
            }

            return CreatedAtAction("GetHoiDap", new { id = hoiDap.MaCauHoi }, hoiDap);
        }

        // DELETE: api/HoiDaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoiDap(string id)
        {
            bool flag = await _crudService.Delete_HoiDap(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

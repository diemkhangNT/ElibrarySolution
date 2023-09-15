using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Data;
using QuanLyBaiGiang_TaiNguyen.Dto;
using QuanLyBaiGiang_TaiNguyen.Interface;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuDesController : ControllerBase
    {
        private readonly TaiNguyenDbContext _context;
        private readonly ICrudChuDe _crudService;
        private readonly IMapper _mapper;
        public ChuDesController(TaiNguyenDbContext context, ICrudChuDe crudService, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChuDe>>> GetChuDe()
        {
            if (_context.ChuDes == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_ChuDes();
            return listLTB.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChuDe>> GetChuDe(string id)
        {
            ChuDe chuDe = await _crudService.Get_ChuDe(id);
            if (chuDe == null)
            {
                return NotFound("Không tìm thấy chủ đề có id = " + id + "!");
            }
            else return chuDe;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChuDe(string id, [FromForm] ChuDeDto chuDeDto)
        {
            ChuDe chuDe = _mapper.Map<ChuDe>(chuDeDto);
            if (!_crudService.ChuDeExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                chuDe.MaCD = id;
                await _crudService.Put_ChuDe(chuDe);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.ChuDeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ChuDe>> PostChuDe([FromForm] ChuDeDto chuDeDto)
        {
            ChuDe chuDe = _mapper.Map<ChuDe>(chuDeDto);
            if (_context.ChuDes == null)
            {
                return Problem("Entity set 'TaiNguyenDbContext.ChuDe'  is null.");
            }
            try
            {
                await _crudService.Post_ChuDe(chuDe);
            }
            catch (DbUpdateException)
            {
                if (_crudService.ChuDeExists(chuDe.MaCD))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetChuDe", new { id = chuDe.MaCD }, chuDe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChuDe(string id)
        {
            bool flag = await _crudService.Delete_ChuDe(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

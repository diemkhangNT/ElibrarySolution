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
    public class LopHocsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly ICrudLopHoc _crudService;
        private readonly IMapper _mapper;

        public LopHocsController(MonHocDbContext context, IExtension extension, IMapper mapper, ICrudLopHoc crudService)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/LopHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LopHoc>>> GetLopHoc()
        {
            if (_context.LopHocs == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_LopHocs();
            return listLTB.ToList();
        }

        // GET: api/LopHocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LopHoc>> GetLopHoc(string id)
        {
            LopHoc lopHoc = await _crudService.Get_LopHoc(id);
            if (lopHoc == null)
            {
                return NotFound("Không tìm thấy lớp học có id = " + id + "!");
            }
            else return lopHoc;
        }

        // PUT: api/LopHocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLopHoc(string id, [FromForm] LopHocDto lopHocDto)
        {
            LopHoc lopHoc = _mapper.Map<LopHoc>(lopHocDto);
            if (id != lopHoc.MaLop)
            {
                return BadRequest();
            }
            try
            {
                lopHoc.MaLop = id;
                await _crudService.Put_LopHoc(lopHoc);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extension.LopHocExists(id))
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

        // POST: api/LopHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LopHoc>> PostLopHoc([FromForm] LopHocDto lopHocDto)
        {
            LopHoc lopHoc = _mapper.Map<LopHoc>(lopHocDto);
          if (_context.LopHocs == null)
          {
              return Problem("Entity set 'MonHocDbContext.LopHoc'  is null.");
          }
            try
            {
                _extension.AutoPK_LopHoc(lopHoc);
                await _crudService.Post_LopHoc(lopHoc);
            }
            catch (DbUpdateException)
            {
                if ( _extension.LopHocExists(lopHoc.MaLop))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLopHoc", new { id = lopHoc.MaLop }, lopHoc);
        }

        // DELETE: api/LopHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLopHoc(string id)
        {
            bool flag = await _crudService.Delete_LopHoc(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

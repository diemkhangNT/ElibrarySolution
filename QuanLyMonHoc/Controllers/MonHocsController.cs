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
    public class MonHocsController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly ICrudMonHoc _crudService;
        private readonly IMapper _mapper;
        public MonHocsController(MonHocDbContext context, IExtension extension, IMapper mapper, ICrudMonHoc crudService)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/MonHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonHoc>>> GetMonHocs()
        {
            if (_context.MonHocs == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_MonHocs();
            return listLTB.ToList();
        }

        // GET: api/MonHocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonHoc>> GetMonHoc(string id)
        {
            MonHoc monHoc = await _crudService.Get_MonHoc(id);
            if (monHoc == null)
            {
                return NotFound("Không tìm thấy môn học có id = " + id + "!");
            }
            else return monHoc;
        }

        // PUT: api/MonHocs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonHoc(string id, [FromForm] MonHocDto monHocDto)
        {
            MonHoc monHoc = _mapper.Map<MonHoc>(monHocDto);
            if (!_extension.MonHocExists(id))
            {
                return NotFound("Không tìm thấy id!");
            }
            if (_extension.IsExistNameMonHoc_Put(monHoc))
            {
                try
                {
                    monHoc.MaMH = id;
                    await _crudService.Put_MonHoc(monHoc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_extension.MonHocExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else return BadRequest("Tên môn học này đã tồn tại!");
            return NoContent();
        }

        // POST: api/MonHocs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonHoc>> PostMonHoc([FromForm] MonHocDto monHocdto)
        {
            MonHoc monHoc = _mapper.Map<MonHoc>(monHocdto);
          if (_context.MonHocs == null)
          {
              return Problem("Entity set 'MonHocDbContext.MonHocs'  is null.");
          }
            else
            {
                if (_extension.IsExistNameMonHoc_Post(monHoc.TenMH)){
                    return BadRequest("Tên môn học này đã tồn tại!");
                }
                else
                {
                    try
                    {
                        _extension.AutoPK_MonHoc(monHoc);
                        await _crudService.Post_MonHoc(monHoc);
                    }
                    catch (DbUpdateException)
                    {
                        if (_extension.MonHocExists(monHoc.MaMH))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return CreatedAtAction("GetMonHoc", new { id = monHoc.MaMH }, monHoc);
        }

        // DELETE: api/MonHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonHoc(string id)
        {
            bool flag = await _crudService.Delete_MonHoc(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

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
    public class NienKhoasController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly ICrudNienKhoa _crudService;
        private readonly IMapper _mapper;
        public NienKhoasController(MonHocDbContext context, IExtension extension, ICrudNienKhoa crudService, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _crudService = crudService;
            _mapper = mapper;
        }

        // GET: api/NienKhoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NienKhoa>>> GetNienKhoa()
        {
            if (_context.NienKhoas == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_NienKhoas();
            return listLTB.ToList();
        }

        // GET: api/NienKhoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NienKhoa>> GetNienKhoa(string id)
        {
            NienKhoa nienKhoa = await _crudService.Get_NienKhoa(id);
            if (nienKhoa == null)
            {
                return NotFound("Không tìm thấy niên khóa có id = " + id + "!");
            }
            else return nienKhoa;
        }

        // PUT: api/NienKhoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNienKhoa(string id, [FromForm] NienKhoaDto nienKhoaDto)
        {
            NienKhoa nienKhoa = _mapper.Map<NienKhoa>(nienKhoaDto);
            if (!_extension.NienKhoaExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            if(_extension.IsCheckTime_put(nienKhoa.TGBatDau, nienKhoa.TGKetThuc, nienKhoa.MaNK))
            {
                try
                {
                    nienKhoa.MaNK = id;
                    await _crudService.Put_NienKhoa(nienKhoa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_extension.NienKhoaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest("Thời gian không hợp lệ! ");
            }
            return NoContent();
        }

        // POST: api/NienKhoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NienKhoa>> PostNienKhoa([FromForm] NienKhoaDto nienKhoaDto)
        {
            NienKhoa nienKhoa = _mapper.Map<NienKhoa>(nienKhoaDto);
            if (_context.NienKhoas == null)
          {
              return Problem("Entity set 'MonHocDbContext.NienKhoa'  is null.");
          }
          if(_extension.IsCheckTime(nienKhoa.TGBatDau, nienKhoa.TGKetThuc))
            {
                try
                {
                    _extension.AutoPK_NienKhoa(nienKhoa);
                    await _crudService.Post_NienKhoa(nienKhoa);
                }
                catch (DbUpdateException)
                {
                    if (_extension.NienKhoaExists(nienKhoa.MaNK))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
          else return BadRequest("Thời gian không hợp lệ! ");


            return CreatedAtAction("GetNienKhoa", new { id = nienKhoa.MaNK }, nienKhoa);
        }

        // DELETE: api/NienKhoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNienKhoa(string id)
        {
            bool flag = await _crudService.Delete_NienKhoa(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

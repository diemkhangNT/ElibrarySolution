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
    public class TraLoisController : ControllerBase
    {
        private readonly MonHocDbContext _context;
        private readonly IExtension _extension;
        private readonly ICrudTraLoi _crudService;
        private readonly IMapper _mapper;

        public TraLoisController(MonHocDbContext context, IExtension extension, IMapper mapper, ICrudTraLoi crudService)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
            _crudService = crudService;
        }

        // GET: api/TraLois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraLoi>>> GetTraLoi()
        {
            if (_context.MonHocs == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_TraLois();
            return listLTB.ToList();
        }

        // GET: api/TraLois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraLoi>> GetTraLoi(string id)
        {
            TraLoi traLoi = await _crudService.Get_TraLoi(id);
            if (traLoi == null)
            {
                return NotFound("Không tìm thấy câu trả lời có id = " + id + "!");
            }
            else return traLoi;
        }

        // PUT: api/TraLois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraLoi(string id, [FromForm] TraLoiDto traLoiDto)
        {
            TraLoi traLoi = _mapper.Map<TraLoi>(traLoiDto);
            if (!_extension.TraLoiExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                traLoi.MaCauTL = id;
                await _crudService.Put_TraLoi(traLoi);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_extension.TraLoiExists(id))
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

        // POST: api/TraLois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraLoi>> PostTraLoi([FromForm] TraLoiDto traLoiDto)
        {
            TraLoi traLoi = _mapper.Map<TraLoi>(traLoiDto);
            if (_context.TraLois == null)
          {
              return Problem("Entity set 'MonHocDbContext.TraLoi'  is null.");
          }
            try
            {
                _extension.AutoPK_TraLoi(traLoi);
                await _crudService.Post_TraLoi(traLoi);
            }
            catch (DbUpdateException)
            {
                if (_extension.TraLoiExists(traLoi.MaCauTL))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraLoi", new { id = traLoi.MaCauTL }, traLoi);
        }

        // DELETE: api/TraLois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraLoi(string id)
        {
            bool flag = await _crudService.Delete_TraLoi(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }

    }
}

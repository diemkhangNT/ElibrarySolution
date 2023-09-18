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
    public class TraLoiTLsController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly ICrudTraLoiTuLuan _crudService;
        private readonly IFileExtention _fileExtention;
        private readonly IMapper _mapper;

        public TraLoiTLsController(DeThiDbContext context, ICrudTraLoiTuLuan crudService, IMapper mapper, IFileExtention fileExtention)
        {
            _context = context;
            _crudService = crudService;
            _mapper = mapper;
            _fileExtention = fileExtention;
        }

        // GET: api/TraLoiTLs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TraLoiTL>>> GetCauTraLoiTLs()
        {
            if (_context.CauTraLoiTLs == null)
            {
                return NotFound();
            }
            var listLTB = await _crudService.Get_TraLoiTLs();
            return listLTB.ToList();
        }

        // GET: api/TraLoiTLs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TraLoiTL>> GetTraLoiTL(string id)
        {
            TraLoiTL tL = await _crudService.Get_TraLoiTL(id);
            if (tL == null)
            {
                return NotFound("Không tìm thấy câu trả lời tự luận có id = " + id + "!");
            }
            else return tL;
        }

        // PUT: api/TraLoiTLs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraLoiTL(string id, [FromForm] TraLoiTuLuanDto traLoiTL)
        {
            TraLoiTL tL = _mapper.Map<TraLoiTL>(traLoiTL);
            if (!_crudService.TraLoiTLExists(id))
            {
                return NotFound("Không tìm thấy!");
            }
            try
            {
                tL.MaCH = id;
                await _crudService.Put_TraLoiTL(tL);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.TraLoiTLExists(id))
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

        // POST: api/TraLoiTLs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TraLoiTL>> PostTraLoiTL([FromForm] TraLoiTuLuanDto traLoiTL, IFormFile? FileCauTL, CancellationToken cancellationToken)
        {
            TraLoiTL tL = _mapper.Map<TraLoiTL>(traLoiTL);
            if (_context.CauTraLoiTLs == null)
          {
              return Problem("Entity set 'DeThiDbContext.CauTraLoiTLs'  is null.");
          }
            try
            {
                if (_crudService.Check_HinhThucTL(traLoiTL.MaCH))
                {
                    if (FileCauTL == null)
                        return BadRequest("Vui lòng upload file câu trả lời!");
                    var result = await _fileExtention.WriteFile(FileCauTL, "DapAnTuLuan");
                    if (result == "")
                        return BadRequest("File đáp án chỉ chứa .docx, .doc hoặc pdf!");
                    tL.FileCauTL = result;
                    tL.CauTL = null;
                    await _crudService.Post_TraLoiTL(tL);
                }
                else if(tL.CauTL == null)
                {
                    return BadRequest("Vui lòng nhập câu trả lời!");
                }
                else
                {
                    tL.FileCauTL = null;
                    await _crudService.Post_TraLoiTL(tL);
                }
            }
            catch (DbUpdateException)
            {
                if (_crudService.TraLoiTLExists(tL.MaCH))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTraLoiTL", new { id = tL.MaCH }, tL);
        }

        // DELETE: api/TraLoiTLs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraLoiTL(string id)
        {
            bool flag = await _crudService.Delete_TraLoiTL(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

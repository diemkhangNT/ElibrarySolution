using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Dto;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeThisController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly ICrudDeThi _crudService;
        private readonly IFileExtention _fileExtention;
        private readonly IMapper _mapper;
        public DeThisController(DeThiDbContext context, ICrudDeThi crudService, IFileExtention fileExtention, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _fileExtention = fileExtention;
            _mapper = mapper;
        }

        // GET: api/DeThis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeThi>>> GetDeThis()
        {
            if (_context.DeThis == null)
            {
                return NotFound();
            }
            var deThis = await _crudService.Get_DeThis();
            return deThis.ToList();
        }

        // GET: api/DeThis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeThi>> GetDeThi(string id)
        {
            DeThi deThi = await _crudService.Get_DeThi(id);
            if (!_crudService.DeThiExists(id))
            {
                return NotFound("Không tìm thấy tệp có mã = " + id + "!");
            }
            return deThi;
        }

        [HttpGet]
        [Route("DownloadByFileName")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DeThi", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";

            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        // PUT: api/DeThis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeThi(string id, [FromForm] DeThiDto deThiDto)
        {
            DeThi DeThis = _mapper.Map<DeThi>(deThiDto);
            if (!_crudService.DeThiExists(id))
            {
                return BadRequest("Không tồn tại mã đề thi này!");
            }
            try
            {
                DeThis.MaDT = id;
                await _crudService.Put_DeThi(DeThis);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.DeThiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Thông tin đã được cập nhật!");
        }

        [HttpPut]
        [Route("PheDuyetDeThi")]
        public async Task<IActionResult> PheDuyetDeThi(string id)
        {
            if (!_crudService.DeThiExists(id))
            {
                return BadRequest("Không tồn tại mã đề thi này!");
            }
            try
            {
                await _crudService.XetDuyet_DeThi(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.DeThiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Đề thi này đã được xét duyệt!");
        }

        // POST: api/DeThis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeThi>> PostDeThi([FromForm] DeThiDto deThiDto, IFormFile? file, CancellationToken cancellationToken)
        {
            DeThi DeThis = _mapper.Map<DeThi>(deThiDto);
            try
            {
                var result = "";
                if(file != null)
                {
                    result = await _fileExtention.WriteFile(file, "DeThi");
                    if (result == "")
                        return BadRequest("Đề thi không chứa tệp định dạng video!");
                }
                DeThis.FileDeThi = result.ToString();
                await _crudService.Post_DeThi(DeThis);
                return Ok(DeThis);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/DeThis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiNguyen(string id)
        {
            bool flag = await _crudService.Delete_DeThi(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

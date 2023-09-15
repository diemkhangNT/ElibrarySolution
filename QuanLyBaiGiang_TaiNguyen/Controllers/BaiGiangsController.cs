using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using QuanLyBaiGiang_TaiNguyen.Data;
using QuanLyBaiGiang_TaiNguyen.Dto;
using QuanLyBaiGiang_TaiNguyen.Interface;
using QuanLyBaiGiang_TaiNguyen.Model;

namespace QuanLyBaiGiang_TaiNguyen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiGiangsController : ControllerBase
    {
        private readonly TaiNguyenDbContext _context;
        private readonly ICrudBaiGiang _crudService;
        private readonly IFileExtension _fileExtention;
        private readonly IMapper _mapper;
        public BaiGiangsController(TaiNguyenDbContext context, ICrudBaiGiang crudService, IFileExtension fileExtention, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _fileExtention = fileExtention;
            _mapper = mapper;
        }

        // GET: api/TaiNguyens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaiGiang>>> GetBaiGiangs()
        {
            if (_context.BaiGiangs == null)
            {
                return NotFound();
            }
            var baiGiang = await _crudService.Get_BaiGiangs();
            return baiGiang.ToList();
        }

        // GET: api/TaiNguyens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaiGiang>> GetBaiGiang(string id)
        {
            BaiGiang baiGiang = await _crudService.Get_BaiGiang(id);
            if (!_crudService.BaiGiangExists(id))
            {
                return NotFound("Không tìm thấy tệp có mã = " + id + "!");
            }
            return baiGiang;
        }

        [HttpGet]
        [Route("DownloadByFileName")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\BaiGiang", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";

            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        // PUT: api/TaiNguyens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaiGiang(string id, [FromForm] BaiGiangDto baiGiangDto)
        {
            BaiGiang baiGiang = _mapper.Map<BaiGiang>(baiGiangDto);
            if (!_crudService.BaiGiangExists(id))
            {
                return BadRequest("Không tồn tại mã tài nguyên này!");
            }
            try
            {
                baiGiang.MaBG = id;
                await _crudService.Put_BaiGiang(baiGiang);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.BaiGiangExists(id))
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
        [Route("PheDuyetTaiNguyen")]
        public async Task<IActionResult> PheDuyetBaiGiang(string id)
        {
            if (!_crudService.BaiGiangExists(id))
            {
                return BadRequest("Không tồn tại mã tài nguyên này!");
            }
            try
            {
                await _crudService.XetDuyet_BaiGiang(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.BaiGiangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Bài giảng đã được xét duyệt!");
        }

        // POST: api/TaiNguyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaiGiang>> PostBaiGiang([FromForm] BaiGiangDto baiGiangDto, IFormFile file, CancellationToken cancellationToken)
        {
            BaiGiang baiGiang = _mapper.Map<BaiGiang>(baiGiangDto);
            try
            {
                var result = await _fileExtention.WriteFile(file, "BaiGiang");
                if (result == "")
                    return BadRequest("Bài giảng chỉ chứa tệp định dạng video! Vui lòng chọn thêm tài nguyên để thêm những file khác.");
                baiGiang.TenFile = result.ToString();
                await _crudService.Post_BaiGiang(baiGiang, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/TaiNguyens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiNguyen(string id)
        {
            bool flag = await _crudService.Delete_BaiGiang(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

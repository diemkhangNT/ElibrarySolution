using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TaiNguyensController : ControllerBase
    {
        private readonly TaiNguyenDbContext _context;
        private readonly ICrudTaiNguyen _crudService;
        private readonly IFileExtension _fileExtention;
        private readonly IMapper _mapper;
        public TaiNguyensController(TaiNguyenDbContext context, ICrudTaiNguyen crudService, IFileExtension fileExtention, IMapper mapper)
        {
            _context = context;
            _crudService = crudService;
            _fileExtention = fileExtention;
            _mapper = mapper;
        }

        // GET: api/TaiNguyens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiNguyen>>> GetTaiNguyens()
        {
            if (_context.TaiNguyens == null)
            {
                return NotFound();
            }
            var taiNguyens = await _crudService.Get_TaiNguyens();
            return taiNguyens.ToList();
        }

        // GET: api/TaiNguyens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiNguyen>> GetTaiNguyen(string id)
        {
            TaiNguyen taiNguyen = await _crudService.Get_TaiNguyen(id);
            if (!_crudService.TaiNguyenExists(id))
            {
                return NotFound("Không tìm thấy tệp có mã = " + id + "!");
            }
            return taiNguyen;
        }

        [HttpGet]
        [Route("DownloadByFileName")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\TaiNguyen", fileName);
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
        public async Task<IActionResult> PutTaiNguyen(string id,[FromForm] TaiNguyenDto taiNguyenDto)
        {
            TaiNguyen taiNguyen = _mapper.Map<TaiNguyen>(taiNguyenDto);
            if (!_crudService.TaiNguyenExists(id))
            {
                return BadRequest("Không tồn tại mã tài nguyên này!");
            }
            try
            {
                taiNguyen.MaTN = id;
                await _crudService.Put_TaiNguyen(taiNguyen);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.TaiNguyenExists(id))
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
        public async Task<IActionResult> PheDuyetTaiNguyen(string id)
        {
            if (!_crudService.TaiNguyenExists(id))
            {
                return BadRequest("Không tồn tại mã tài nguyên này!");
            }
            try
            {
                await _crudService.XetDuyet_TaiNguyen(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_crudService.TaiNguyenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Tài nguyên đã được xét duyệt!");
        }

        // POST: api/TaiNguyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiNguyen>> PostTaiNguyen([FromForm] TaiNguyenDto taiNguyenDto, IFormFile file, CancellationToken cancellationToken)
        {
            TaiNguyen taiNguyen = _mapper.Map<TaiNguyen>(taiNguyenDto);
            try
            {
                var result = await _fileExtention.WriteFile(file, "TaiNguyen");
                if (result == "")
                    return BadRequest("Tài nguyên không chứa tệp định dạng video! Vui lòng chọn thêm bài giảng để thêm video.");
                taiNguyen.TenFile = result.ToString();
                await _crudService.Post_TaiNguyen(taiNguyen, file);
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
            bool flag = await _crudService.Delete_TaiNguyen(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using QuanLyTepRiengTu.Data;
using QuanLyTepRiengTu.Dto;
using QuanLyTepRiengTu.Interface;
using QuanLyTepRiengTu.Model;

namespace QuanLyTepRiengTu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilePrivatesController : ControllerBase
    {
        private readonly FilePrivateDbContext _dbContext;
        private readonly IFileCrudService _fileCrudService;
        private readonly IMapper _mapper;

        public FilePrivatesController(IFileCrudService fileCrudService, FilePrivateDbContext dbContext, IMapper mapper)
        {
            _fileCrudService = fileCrudService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(typeof(string), statusCode:400)]
        public async Task<IActionResult> UploadFile([FromForm] TepRiengTuDto tepRiengDto ,IFormFile file, CancellationToken cancellationToken)
        {
            TepRiengTu tepRiengTu = _mapper.Map<TepRiengTu>(tepRiengDto);
            try
            {
                var result = await _fileCrudService.WriteFile(file);
                tepRiengTu.Url = "~/UploadFiles/" + result.ToString();
                _fileCrudService.Post_Data(tepRiengTu, file);
                return Ok(result);
            }catch (Exception ex) 
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TepRiengTu>>> GetTepRiengTus()
        {
            if (_dbContext.tepRiengTus == null)
            {
                return NotFound();
            }
            var tepRTs = await _fileCrudService.Get_TepRiengTus();
            return tepRTs.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TepRiengTu>> GetTepRiengTu(int id)
        {
            TepRiengTu tepRT = await _fileCrudService.Get_TepRiengTu(id);
            if (tepRT == null)
            {
                return NotFound("Không tìm thấy tệp có STT = " + id + "!");
            }
            return tepRT;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiangVien(int id, TepRiengTuDto tepRiengTuDto)
        {
            TepRiengTu tepRT = _mapper.Map<TepRiengTu>(tepRiengTuDto);
            if (!_fileCrudService.TepRTExists(id))
            {
                return BadRequest("Không tồn tại mã tệp này!");
            }
            try
            {
                tepRT.STT = id;
                await _fileCrudService.Put_TepRiengTu(tepRT);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_fileCrudService.TepRTExists(id))
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

        [HttpGet]
        [Route("DownloadByFileName")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UploadFiles", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";

            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}

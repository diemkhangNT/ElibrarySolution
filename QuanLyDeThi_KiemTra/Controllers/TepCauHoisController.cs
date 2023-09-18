using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using QuanLyDeThi_KiemTra.Data;
using QuanLyDeThi_KiemTra.Interface;
using QuanLyDeThi_KiemTra.Model;

namespace QuanLyDeThi_KiemTra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TepCauHoisController : ControllerBase
    {
        private readonly DeThiDbContext _context;
        private readonly IFileExtention _fileExtention;
        private readonly ICrudTepCauHoi _crudService;

        public TepCauHoisController(DeThiDbContext context, IFileExtention fileExtention, ICrudTepCauHoi crudService)
        {
            _context = context;
            _fileExtention = fileExtention;
            _crudService = crudService;
        }

        [HttpPost]
        public async Task<ActionResult<TepCauhoi>> UploadFileCauhoi(IFormFile file, CancellationToken cancellationToken)
        {
            TepCauhoi tepCauhoi = new TepCauhoi();
            try
            {
                var result = await _fileExtention.WriteFile(file, "CauHoi");
                if (result == "")
                    return BadRequest("Tệp câu chỉ chứa tệp định dạng excel!");
                tepCauhoi.TenTep = result.ToString();
                await _crudService.Post_TepCauhoi(tepCauhoi);
                return Ok(tepCauhoi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("DownloadByFileName")]
        public async Task<IActionResult> DownloadFile(int stt)
        {
            TepCauhoi tepCauhoi = await _crudService.Get_TepCauhoi(stt);
            if (tepCauhoi == null) return NotFound();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CauHoi", tepCauhoi.TenTep);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";

            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTepCauHoi(int id)
        {
            bool flag = await _crudService.Delete_TepCauhoi(id);
            if (!flag)
            {
                return NotFound("Không tìm thấy!");
            }
            else return Ok("Đã xóa thành công!");
        }
    }
}

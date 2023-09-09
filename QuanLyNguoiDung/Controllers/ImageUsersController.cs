using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Model;
using QuanLyNguoiDung.Services;
using System.Collections.Generic;

namespace QuanLyNguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUsersController : ControllerBase
    {
        private readonly UserDBContext _context;
        private readonly IExtensionServices _extensionServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUsersController(UserDBContext context, IExtensionServices extensionServices, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _extensionServices = extensionServices;
            _webHostEnvironment = webHostEnvironment;
        }

        //[HttpPost]
        //public async Task<string> PostImage([FromForm] FileUpload fileUpload)
        //{
        //    try
        //    {
        //        if (fileUpload.formFile.Length > 0)
        //        {
        //            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            using (FileStream fileStream = System.IO.File.Create(path + fileUpload.formFile.FileName))
        //            {
        //                fileUpload.formFile.CopyTo(fileStream);
        //                fileStream.Flush();
        //                return "Đã lưu hình ảnh.";
        //            }
        //        }
        //        else return "Đã có lỗi xảy ra.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //
        [HttpGet("gethinh")]
        public async Task<IActionResult> GetImage( string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }
        //
    }
}

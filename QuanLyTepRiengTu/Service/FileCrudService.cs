using Microsoft.EntityFrameworkCore;
using QuanLyTepRiengTu.Data;
using QuanLyTepRiengTu.Interface;
using QuanLyTepRiengTu.Model;

namespace QuanLyTepRiengTu.Service
{
    public class FileCrudService : IFileCrudService
    {
        private readonly FilePrivateDbContext _context;

        public FileCrudService(FilePrivateDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete_TepRiengTu(int stt)
        {
            if (_context.tepRiengTus == null)
            {
                return false;
            }
            var tepRT = await _context.tepRiengTus.FindAsync(stt);
            if (tepRT == null)
            {
                return false;
            }
            _context.tepRiengTus.Remove(tepRT);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TepRiengTu> Get_TepRiengTu(int stt)
        {
            var tepRT = await _context.tepRiengTus.FindAsync(stt);
            return tepRT;
        }

        public async Task<IEnumerable<TepRiengTu>> Get_TepRiengTus()
        {
            return await _context.tepRiengTus.ToListAsync();
        }

        public async Task<TepRiengTu> Put_TepRiengTu(TepRiengTu tepRiengTu)
        {
            var existTepRT = _context.tepRiengTus.FirstOrDefault(x => x.STT == tepRiengTu.STT);
            tepRiengTu.KichThuoc = existTepRT.KichThuoc;
            tepRiengTu.Url = existTepRT.Url;
            tepRiengTu.NguoiTao = existTepRT.NguoiTao;
            tepRiengTu.NgayCapNhat = DateTime.Now;
            tepRiengTu.TheLoai = existTepRT.TheLoai;
            _context.tepRiengTus.Remove(existTepRT);
            //_context.SaveChanges();
            _context.Entry(tepRiengTu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tepRiengTu;
        }

        public void Post_Data(TepRiengTu tepRiengTu, IFormFile formFile)
        {
            tepRiengTu.NgayCapNhat = DateTime.Now;
            tepRiengTu.NguoiTao = "Leadership";
            tepRiengTu.KichThuoc = SizeFile(formFile);
            var extention = formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
            tepRiengTu.TheLoai = extention;
            _context.tepRiengTus.Add(tepRiengTu);
            _context.SaveChanges();
        }

        public double SizeFile(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                double sizeMB = (double)file.Length / 1048576;
                return Math.Round(sizeMB,1);
            }else { return 0; }
        }

        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extention = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks.ToString() + extention;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UploadFiles");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UploadFiles", fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool TepRTExists(int stt)
        {
            return (_context.tepRiengTus?.Any(e => e.STT == stt)).GetValueOrDefault();
        }
    }
}

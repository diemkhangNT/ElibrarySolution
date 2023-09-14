using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Data;
using QuanLyNguoiDung.Interface;
using QuanLyNguoiDung.Model;
using System.Text.RegularExpressions;

namespace QuanLyNguoiDung.Services
{
    public class ExtensionServices : IExtensionServices
    {
        private readonly UserDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ExtensionServices(UserDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public void AutoPK_GiangVien(GiangVien giangVien)
        {
            Random rnd = new Random();
            string num = rnd.Next(100, 1000000000).ToString();
            giangVien.MaGV = "#GV" + num;
            giangVien.NgayHopTac = DateTime.Now;
            giangVien.TrangThaiHD = true;
        }

        public void AutoPK_HocVien(HocVien hocVien)
        {
            Random rnd = new Random();
            string num = rnd.Next(100, 1000000000).ToString();
            hocVien.MaHV = "#HV" + num;
            hocVien.NgayTao = DateTime.Now;
        }

        public bool ValidatePassword(string password)
        {
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$";
            bool isValid = Regex.IsMatch(password, passwordRegex);
            return isValid;
        }

        public bool IsEmailGVUnique(string emailGV)
        {
            return _dbContext.GiangViens.Any(u => u.Email == emailGV);
        }
        public bool IsEmailHVUnique(string emailHV)
        {
            return _dbContext.HocViens.Any(u => u.Email == emailHV);
        }

        public bool IsUserNameGVUnique(string username)
        {
            return _dbContext.GiangViens.Any(u => u.Username == username);
        }
        public bool IsUserNameHVUnique(string username)
        {
            return _dbContext.HocViens.Any(u => u.Username == username);
        }

        public bool IsNumberPhone(string sdt)
        {
            string input = sdt;
            bool isNumber = Regex.IsMatch(input, "^[0-9]+$");
            if (isNumber)
            {
                // Biểu thức chính quy kiểm tra số điện thoại
                string pattern = @"^(0|\+84)(\d{9,10})$";

                // Kiểm tra tính hợp lệ của số điện thoại
                return Regex.IsMatch(input, pattern);
            }
            return isNumber;
        }

        public void UploadImageGV(GiangVien giangVien, IFormFile imageFile)
        {
            if (imageFile.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + imageFile.FileName))
                {
                    imageFile.CopyTo(fileStream);
                    fileStream.Flush();
                    giangVien.HinhDaiDien = imageFile.FileName;
                }
            }
            else
            {
                giangVien.HinhDaiDien = null;
            }
        }

        public void UploadImageHV(HocVien hocVien, IFormFile imageFile)
        {
            if (imageFile.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + imageFile.FileName))
                {
                    imageFile.CopyTo(fileStream);
                    fileStream.Flush();
                    hocVien.AnhDaiDien = imageFile.FileName;
                }
            }
            else
            {
                hocVien.AnhDaiDien = null;
            }
        }

        public void UploadImageLD(Leadership leadership, IFormFile imageFile)
        {
            if (imageFile.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + imageFile.FileName))
                {
                    imageFile.CopyTo(fileStream);
                    fileStream.Flush();
                    leadership.HinhDaiDien = imageFile.FileName;
                }
            }
            else
            {
                leadership.HinhDaiDien = null;
            }
        }

        public bool GetImageById(string id)
        {
            throw new NotImplementedException();
        }

        public bool HocVienExists(string id)
        {
            return (_dbContext.HocViens?.Any(e => e.MaHV == id)).GetValueOrDefault();
        }

        public bool GiangVienExists(string id)
        {
            return (_dbContext.GiangViens?.Any(e => e.MaGV == id)).GetValueOrDefault();
        }

        public void AutoPK_Leader(Leadership leadership)
        {
            Random rnd = new Random();
            string num = rnd.Next(100, 1000000000).ToString();
            leadership.MaLD = "#LD" + num;
            leadership.NgayHopTac = DateTime.Now;
            leadership.TrangThaiHD = true;
        }

        public bool IsEmailLDUnique(string emailLD)
        {
            return _dbContext.leaderships.Any(u => u.Email == emailLD);
        }

        public bool IsUserNameLDUnique(string username)
        {
            return _dbContext.leaderships.Any(u => u.Username == username);
        }

        public bool LeadershipExists(string id)
        {
            return (_dbContext.leaderships?.Any(e => e.MaLD == id)).GetValueOrDefault();
        }
    }
}

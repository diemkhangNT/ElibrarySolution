using Microsoft.EntityFrameworkCore;
using QuanLyNguoiDung.Model;

namespace QuanLyNguoiDung.Interface
{
    public interface IExtensionServices
    {
        public void AutoPK_GiangVien(GiangVien giangVien);
        public void AutoPK_HocVien(HocVien hocVien);
        bool ValidatePassword(string password);
        bool IsEmailGVUnique(string emailGV);
        bool IsEmailHVUnique(string emailHV);
        bool IsNumberPhone(string sdt);
        bool IsUserNameGVUnique(string username);
        bool IsUserNameHVUnique(string username);
        bool HocVienExists(string id);
        bool GiangVienExists(string id);
        //image
        public void UploadImageGV(GiangVien giangVien, IFormFile imageFile);
        public void UploadImageHV(HocVien hocVien, IFormFile imageFile);
        public bool GetImageById(string fileName);
    }
}
